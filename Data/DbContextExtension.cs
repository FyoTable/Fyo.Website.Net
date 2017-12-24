using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using Newtonsoft.Json;
using Fyo.Models;
using Fyo.Enums;
using Microsoft.Extensions.Configuration;

namespace Fyo
{
    public static class DbContextExtension
    {        
        public static bool AllMigrationsApplied(this DbContext context)
        {
            return !context.Database.GetPendingMigrations().Any();
        }

        public static void EnsureSeeded(this DataContext context, IConfiguration configuration)
        { 
        //     var seedInfo = configuration.GetSection("SeedInfo");

        //     if (!context.Languages.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Languages");
        //         context.Languages.Add(new Language() {
        //             Code = "EN-US",
        //             Name = "English"
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.Tenants.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Tenants");
        //         context.Tenants.Add(new Tenant() {
        //              Name = "Default"
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.Companies.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Companies");
        //         context.Companies.Add(new Company() {
        //             Name = "Default",
        //             Tenant = context.Tenants.First()
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.Teams.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Seeds");
        //         context.Teams.Add(new Team() {
        //             Name = "Default",
        //             Company = context.Companies.First()
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.Users.Any()){
        //         var superAdminIds = seedInfo["SuperAdmins"].Split(';');
        //         var team = context.Teams.Include("Company").First();

        //         Console.WriteLine("[DATA SEED] Adding Default Users");
        //         context.Users.Add(new User(){
        //             ThirdPartyUserId = superAdminIds[0],
        //             UserRole = UserRole.SuperAdmin,
        //             Company = team.Company,
        //             Language = context.Languages.First(),
        //             Team = team,
        //             Email = "bmeyer@ostusa.com",
        //             FirstName = "Brandon",
        //             LastName = "Meyer"
        //         });
        //         context.Users.Add(new User(){
        //             ThirdPartyUserId = superAdminIds[1],
        //             UserRole = UserRole.SuperAdmin,
        //             Company = team.Company,
        //             Language = context.Languages.First(),
        //             Team = team,
        //             Email = "ghoofman@ostusa.com",
        //             FirstName = "Garrett",
        //             LastName = "Hoofman"
        //         });
        //         context.Users.Add(new User(){
        //             ThirdPartyUserId = superAdminIds[2],
        //             UserRole = UserRole.SuperAdmin,
        //             Company = team.Company,
        //             Language = context.Languages.First(),
        //             Team = team,
        //             Email = "kcross@ostusa.com",
        //             FirstName = "Kristen",
        //             LastName = "Cross"
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.ScoreSystems.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Score Systems");
        //         context.ScoreSystems.Add(new ScoreSystem() {
        //             Name = "MIPS"
        //         });
        //         context.SaveChanges();
        //     }

        //     if (!context.Surveys.Any())
        //     {
        //         Console.WriteLine("[DATA SEED] Adding Default Survey");
        //         var survey = new Survey() {
        //             Name = "Default",
        //             ScoreSystem = context.ScoreSystems.First()
        //         };
        //         context.Surveys.Add(survey);
        //         context.SaveChanges();

        //         { // Base Binary Question
        //             Console.WriteLine("[DATA SEED] Adding Default Question to Survey");
        //             var question = new Question() {
        //                 Survey = survey,
        //                 QuestionType = QuestionType.Binary
        //             };
        //             context.Questions.Add(question);
        //             context.SaveChanges();

        //             var questionText = new QuestionText() {
        //                 Question = question,
        //                 Language = context.Languages.First(),
        //                 Value = "Is OST the coolest?"
        //             };
        //             context.QuestionTexts.Add(questionText);
        //             context.SaveChanges();


        //             var questionOptionTrue = new QuestionOption() {
        //                 Question = question
        //             };
        //             var questionOptionFalse = new QuestionOption() {
        //                 Question = question
        //             };
        //             context.QuestionOptions.Add(questionOptionTrue);
        //             context.QuestionOptions.Add(questionOptionFalse);
        //             context.SaveChanges();

        //             var questionOptionTrueText = new QuestionOptionText() {
        //                 QuestionOption = questionOptionTrue,
        //                 Language = context.Languages.First(),
        //                 Value = "Yes"
        //             };
        //             var questionOptionFalseText = new QuestionOptionText() {
        //                 QuestionOption = questionOptionFalse,
        //                 Language = context.Languages.First(),
        //                 Value = "No"
        //             };
        //             context.QuestionOptionTexts.Add(questionOptionTrueText);
        //             context.QuestionOptionTexts.Add(questionOptionFalseText);
        //             context.SaveChanges();
        //         }

        //     }
        }
    }


}