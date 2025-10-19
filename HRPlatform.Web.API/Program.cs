using HRPlatform.Application.Candidates;
using HRPlatform.Application.Interfaces;
using HRPlatform.Application.Skills;
using HRPlatform.Domain.Entities;
using HRPlatform.Domain.Interfaces;
using HRPlatform.Infrastructure.Data;
using HRPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories and Unit of Work
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Application Services
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        logger.LogInformation("🚀 Starting database migration and seeding...");

        // Apply migrations if they exist
        await context.Database.MigrateAsync();
        logger.LogInformation("✅ Database migrations applied successfully");

        // Check current data
        var existingSkillsCount = await context.Skills.CountAsync();
        var existingCandidatesCount = await context.Candidates.CountAsync();

        logger.LogInformation($"🔍 Found {existingSkillsCount} existing skills and {existingCandidatesCount} existing candidates");

        // FORCE RESEEDING - Remove this condition to always seed
        bool shouldReseed = existingSkillsCount < 40 || existingCandidatesCount < 50;

        if (shouldReseed)
        {
            logger.LogInformation("🔄 Re-seeding database with enhanced data...");

            // Clear existing data
            logger.LogInformation("🧹 Clearing existing data...");
            context.CandidateSkills.RemoveRange(context.CandidateSkills);
            context.Candidates.RemoveRange(context.Candidates);
            context.Skills.RemoveRange(context.Skills);
            await context.SaveChangesAsync();
            logger.LogInformation("✅ Existing data cleared");

            // Now seed fresh data
            await SeedEnhancedData(context);
            logger.LogInformation("✅ Enhanced database seeded successfully!");
        }
        else
        {
            logger.LogInformation("ℹ️ Database already contains sufficient data, skipping seeding.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "💥 Error during database operation");
    }
}

// Add this method outside the main block
async Task SeedEnhancedData(ApplicationDbContext context)
{
    var logger = context.GetService<ILogger<Program>>();
    var random = new Random();

    // Create 40 skills
    var skillNames = new[]
    {
        "C# Programming", "Java Development", "Python Programming", "JavaScript", "TypeScript",
        "PHP Development", "Ruby on Rails", "Go Programming", "Swift Development", "Kotlin Development",
        "ASP.NET Core", "React.js", "Angular", "Vue.js", "Node.js", "HTML5/CSS3", "Bootstrap", "jQuery",
        "SQL Server", "PostgreSQL", "MySQL", "MongoDB", "Entity Framework", "Azure Cloud", "AWS",
        "Docker", "Kubernetes", "English", "German", "French", "Spanish", "Russian", "Project Management",
        "Agile Methodology", "Scrum Master", "Team Leadership", "Problem Solving", "Communication Skills",
        "Git Version Control", "CI/CD"
    };

    var skills = new List<Skill>();
    foreach (var skillName in skillNames)
    {
        var skill = Skill.Create(skillName);
        skills.Add(skill);
    }

    logger.LogInformation($"📝 Creating {skills.Count} skills...");
    await context.Skills.AddRangeAsync(skills);
    await context.SaveChangesAsync();
    logger.LogInformation($"✅ Saved {skills.Count} skills");

    // Create 50 candidates
    var firstNames = new[] { "John", "Jane", "Michael", "Sarah", "David", "Lisa", "Robert", "Emily", "William", "Jessica",
                           "James", "Amanda", "Christopher", "Michelle", "Daniel", "Ashley", "Matthew", "Stephanie", "Joseph", "Nicole",
                           "Andrew", "Elizabeth", "Joshua", "Samantha", "Ryan", "Megan", "Brian", "Lauren", "Jason", "Rachel",
                           "Kevin", "Jennifer", "Eric", "Rebecca", "Steven", "Laura", "Thomas", "Amber", "Timothy", "Melissa",
                           "Richard", "Crystal", "Jeffrey", "Brittany", "Scott", "Tiffany", "Mark", "Heather", "Paul", "Christina" };

    var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
                          "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
                          "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
                          "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
                          "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts" };

    var domains = new[] { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "company.com" };

    var candidates = new List<Candidate>();

    for (int i = 0; i < 50; i++)
    {
        var firstName = firstNames[random.Next(firstNames.Length)];
        var lastName = lastNames[random.Next(lastNames.Length)];
        var email = $"{firstName.ToLower()}.{lastName.ToLower()}@{domains[random.Next(domains.Length)]}";
        var dateOfBirth = new DateTime(random.Next(1970, 2000), random.Next(1, 13), random.Next(1, 28));
        var contactNumber = $"+1-555-{random.Next(100, 1000)}-{random.Next(1000, 10000)}";

        var candidate = Candidate.Create($"{firstName} {lastName}", dateOfBirth, email, contactNumber);
        candidates.Add(candidate);
    }

    logger.LogInformation($"📝 Creating {candidates.Count} candidates...");
    await context.Candidates.AddRangeAsync(candidates);
    await context.SaveChangesAsync();
    logger.LogInformation($"✅ Saved {candidates.Count} candidates");

    // Assign random skills to candidates
    logger.LogInformation("🔗 Assigning skills to candidates...");
    var allSkills = await context.Skills.ToListAsync();
    var candidateSkillsCount = 0;

    foreach (var candidate in candidates)
    {
        var skillCount = random.Next(3, 9);
        var usedSkillIds = new HashSet<int>();

        for (int i = 0; i < skillCount; i++)
        {
            int skillId;
            do
            {
                skillId = random.Next(1, allSkills.Count + 1);
            } while (usedSkillIds.Contains(skillId));

            usedSkillIds.Add(skillId);

            var skill = allSkills.First(s => s.Id == skillId);
            candidate.AddSkill(skill);
            candidateSkillsCount++;
        }
    }

    await context.SaveChangesAsync();
    logger.LogInformation($"✅ Created {candidateSkillsCount} candidate-skill relationships");

    // Final counts
    var finalSkillsCount = await context.Skills.CountAsync();
    var finalCandidatesCount = await context.Candidates.CountAsync();
    var finalRelationshipsCount = await context.CandidateSkills.CountAsync();

    logger.LogInformation($"🎉 SEEDING COMPLETE!");
    logger.LogInformation($"📊 Skills: {finalSkillsCount}");
    logger.LogInformation($"📊 Candidates: {finalCandidatesCount}");
    logger.LogInformation($"📊 Relationships: {finalRelationshipsCount}");
}


app.Run();