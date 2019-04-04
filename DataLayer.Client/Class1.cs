using LiteDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Client
{

    //  public class ConfigurationSettings
    //  {
    //      static ConfigurationSettings()
    //      {
    //          var builder = new ConfigurationBuilder()
    ////  .SetBasePath(env.ContentRootPath)
    //  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    //  .AddEnvironmentVariables();
    //          Configuration = builder.Build();
    //      }

    //  }

    public class Profile
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }
    }

    public class ProfilePreference
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public List<string> Preferences { get; set; }
        public List<string> EventPreferences { get; set; }

        public List<string> DiningPreferences { get; set; }
    }

    public class CompanyPolicy
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string AccountId { get; set; }
        public int FareAllowanceLimit { get; set; }
        public int AccomadationAllowanceLimit { get; set; }
        public int DailyAllowanceLimit { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class CompanyInfo
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string CompanyLogoUrl { get; set; }
        public string Address { get; set; }
  
    }

    public class OneOrder
    {
        public int Id { get; set; }
        public string OneOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        
        public string ProfileId { get; set; }
        public string CompanyId { get; set; }

       // public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string OneOrderNumber { get; set; }
 
        public DateTime OrderDate { get; set; }
        public string RecordLocator { get; set; }
        public string ProductType { get; set; }

    }

    public class EventCategory
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

        public static class DataLoader
    {

        private static string databaseName = @"LocalData.db";
        static DataLoader()
        {
            using (var db = new LiteDatabase(databaseName))
            {
                // Get ProfilePreference collection
                var profileCol = db.GetCollection<Profile>("Profile");
                // Create unique index in ProfileId field
                profileCol.EnsureIndex(x => x.Id, true);

                // Get ProfilePreference collection
                var profilePreference = db.GetCollection<ProfilePreference>("ProfilePreference");
                // Create unique index in ProfileId field
                profilePreference.EnsureIndex(x => x.ProfileId, true);
                
                // Get ProfilePreference collection
                var product = db.GetCollection<Product>("Product");
                // Create unique index in ProfileId field
                product.EnsureIndex(x => x.Id, true);

                // Get ProfilePreference collection
                var orderCol = db.GetCollection<OneOrder>("OneOrder");
                // Create unique index in ProfileId field
                orderCol.EnsureIndex(x => x.ProfileId, true);

                // Get ProfilePreference collection
                var companyInfoCol = db.GetCollection<CompanyInfo>("CompanyInfo");
                // Create unique index in ProfileId field
                companyInfoCol.EnsureIndex(x => x.Id, true);

                var companyPolicyCol = db.GetCollection<CompanyPolicy>("CompanyPolicy");
                // Create unique index in ProfileId field
                companyPolicyCol.EnsureIndex(x => x.CompanyId, true);

                var eventCategoryCol = db.GetCollection<EventCategory>("EventCategory");
                // Create unique index in ProfileId field
                eventCategoryCol.EnsureIndex(x => x.CategoryId, true);
            }

            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(databaseName))
            {
                var profileCol = db.GetCollection<Profile>("Profile");

                if (profileCol.Count() == 0)
                {
                    // Insert new customer document (Id will be auto-incremented)
                    profileCol.Insert(new Profile
                    {
                        EmployeeName = "Kattu Poochi",
                        Designation = "Manager",
                        Email = "KattuPoochi@gmail.com",
                        ContactNo = "999999"
                    });

                    profileCol.Insert(new Profile
                    {
                        EmployeeName = "Kai Pulla",
                        Designation = "Manager",
                        Email = "KaiPulla@gmail.com",
                        ContactNo = "999999"
                    });

                    profileCol.Insert(new Profile
                    {
                        EmployeeName = "katta Durai",
                        Designation = "Manager",
                        Email = "kattaDurai@gmail.com",
                        ContactNo = "999999"
                    });

                    profileCol.Insert(new Profile
                    {
                        EmployeeName = "Ondi Puli",
                        Designation = "Manager",
                        Email = "OndiPuli@gmail.com",
                        ContactNo = "999999"
                    });

                    profileCol.Insert(new Profile
                    {
                        EmployeeName = "Vettu Kili",
                        Designation = "Manager",
                        Email = "VettuKili@gmail.com",
                        ContactNo = "999999"
                    });

                }
                // Get ProfilePreference collection
                var profilePreference = db.GetCollection<ProfilePreference>("ProfilePreference");

                if (profilePreference.Count() == 0)
                {
                    profilePreference.Insert(new ProfilePreference
                    {
                        Preferences = new List<string> { "WiFi", "PremierAccess", "Meals", "CheckIn-Baggage" },
                        ProfileId =  1,
                    });

                    profilePreference.Insert(new ProfilePreference
                    {
                        Preferences = new List<string> { "WiFi" },
                        ProfileId = 2,
                    });

                    profilePreference.Insert(new ProfilePreference
                    {
                        Preferences = new List<string> { "WiFi", "PremierAccess", "Meals", "CheckIn-Baggage" },
                        ProfileId = 3,
                    });

                    profilePreference.Insert(new ProfilePreference
                    {
                        Preferences = new List<string> { "WiFi", "PremierAccess", "Meals", "CheckIn-Baggage" },
                        ProfileId = 4,
                    });

                    profilePreference.Insert(new ProfilePreference
                    {
                        Preferences = new List<string> { "WiFi", "PremierAccess", "Meals", "CheckIn-Baggage" },
                        ProfileId = 5,
                    });

                }
               
                // Get ProfilePreference collection
                var companyInfoCol = db.GetCollection<CompanyInfo>("CompanyInfo");

                if (companyInfoCol.Count() == 0)
                {
                    companyInfoCol.Insert(new CompanyInfo
                    {
                        CompanyName = "Mannar & Co",
                        CompanyLogoUrl = ""

                    });

                    companyInfoCol.Insert(new CompanyInfo
                    {
                        CompanyName = "Mannar & Co",
                        CompanyLogoUrl = ""

                    });
                }

                var companyPolicyCol = db.GetCollection<CompanyPolicy>("CompanyPolicy");

                if (companyPolicyCol.Count() == 0)
                {
                    companyPolicyCol.Insert(new CompanyPolicy
                    {
                        CompanyId = "1",
                        AccomadationAllowanceLimit = 200,
                        FareAllowanceLimit = 500,
                        DailyAllowanceLimit = 100,
                        CurrencyCode = "USD"
                    });

                    companyPolicyCol.Insert(new CompanyPolicy
                    {
                        CompanyId = "2",
                        AccomadationAllowanceLimit = 200,
                        FareAllowanceLimit = 500,
                        DailyAllowanceLimit = 100,
                        CurrencyCode = "USD"
                    });
                }

                var eventCategoryCol = db.GetCollection<EventCategory>("EventCategory");
                if (eventCategoryCol.Count() == 0)
                {
                    eventCategoryCol.Insert(new EventCategory
                    {
                        CategoryId = "103",
                        Name = "Music",
                        Url = "https://www.eventbriteapi.com/v3/categories/103/"
                    });
                }

            }
        }

        public static List<Profile> GetProfiles()
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var profileCol = db.GetCollection<Profile>("Profile");

                return profileCol.FindAll().ToList();
            }
        }
        public static Profile GetProfile(int profileId)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var profileCol = db.GetCollection<Profile>("Profile");

                return profileCol.FindById(profileId);
            }
        }

        public static CompanyInfo GetCompany(int companyId)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<CompanyInfo>("CompanyInfo");

                return col.FindById(companyId);
            }
        }

        public static CompanyPolicy GetCompanyPolicy(int companyId)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<CompanyPolicy>("CompanyPolicy");

                return col.Find(doc => doc.CompanyId == companyId.ToString()).ToList().FirstOrDefault();
            }
        }

        public static ProfilePreference GetProfilePreference(int profileId)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<ProfilePreference>("ProfilePreference");

                return col.Find(doc => doc.ProfileId == profileId).ToList().FirstOrDefault();
            }
        }

        public static OneOrder GetOneOrder(string oneOrder)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<OneOrder>("OneOrder");

                return col.Find(doc => doc.OneOrderNumber == oneOrder).ToList().FirstOrDefault();
            }
        }

        public static List<OneOrder> GetOneOrderByProfile(string profileId)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<OneOrder>("OneOrder");

                return col.Find(doc => doc.ProfileId == profileId).ToList();
            }

        }
        public static List<Product> GetProductByProfileId(string oneOrder)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<Product>("Product");

                return col.Find(doc => doc.ProductType  == oneOrder).ToList();
            }
        }

        public static Profile SaveProfile(Profile profile)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var profileCol = db.GetCollection<Profile>("Profile");

                profileCol.Upsert(profile);
            }
            return profile;
        }

        public static CompanyInfo SaveCompany(CompanyInfo company)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<CompanyInfo>("CompanyInfo");

                col.Upsert(company);
            }
            return company;
        }

        public static ProfilePreference SaveProfilePreference(int profileId, List<string> preferences)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<ProfilePreference>("ProfilePreference");

                var profilePreferenceList =  col.Find(doc => doc.ProfileId == profileId).ToList();

                if(profilePreferenceList.Count == 0)
                {
                   var profilePreference = new ProfilePreference
                    {
                        Preferences = preferences,
                        ProfileId = profileId,
                    };

                    col.Insert(profilePreference);

                    return profilePreference;
                }

                profilePreferenceList[0].Preferences = preferences;

                col.Update(profilePreferenceList[0]);
                return profilePreferenceList[0];
            }
        }

        public static OneOrder SaveOneOrder(OneOrder oneOrder)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<OneOrder>("OneOrder");

                col.Upsert(oneOrder);

                return oneOrder;
            }
        }

        public static Product SaveProduct(Product product)
        {
            using (var db = new LiteDatabase(databaseName))
            {
                var col = db.GetCollection<Product>("Product");

                col.Upsert(product);
                return product;
            }
        }
    }
}
