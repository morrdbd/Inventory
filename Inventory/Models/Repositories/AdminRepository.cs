using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Repositories
{
    public class AdminRepository
    {
        InventoryDBContext db = new InventoryDBContext();

        public List<User_List> UserList(User_SC s, string lang)
        {
            return db.Database.SqlQuery<User_List>("User_List @p0", lang).ToList();
        }

        //public List<Country> CountryList(string lang, bool all_countries = false)
        //{
        //    var query = db.Countries.Where(x => x.IsActive == true)
        //        .Where(x => (x.CountryCode != "IRN" && x.CountryCode != "PAK") || all_countries)
        //        .OrderBy(x => lang == "prs" ? x.DrName : lang == "ps" ? x.PaName : x.EnName);
        //    return query.ToList();
        //}


        //public List<Province> ProvinceList(string lang)
        //{
        //    return db.Provinces.Where(x => x.IsActive == true)
        //        .OrderBy(x => lang == "prs" ? x.DrName : lang == "ps" ? x.PaName : x.EnName).ToList();
        //}

        //public List<District> DistrictList(string lang, string province)
        //{
        //    return db.Districts.Where(x => x.IsActive == true && x.ProvinceCode == province)
        //        .OrderBy(x => lang == "prs" ? x.DrName : lang == "ps" ? x.PaName : x.EnName).ToList();
        //}

        public List<LookupValue> ProductCategoryList(string lang, int groupValueId)
        {
            return db.LookupValues.Where(x => x.IsActive == true && x.GroupValueId == groupValueId)
                .OrderBy(x => lang == "prs" ? x.DrName : lang == "ps" ? x.PaName : x.EnName).OrderBy(l=>l.ForOrdering).ToList();
        }
        public List<LookupValue> LookupValueList(string lang, string lookup_code)
        {
            return db.LookupValues.Where(x => x.IsActive == true && x.LookupCode == lookup_code)
                .OrderBy(x => lang == "prs" ? x.DrName : lang == "ps" ? x.PaName : x.EnName).OrderBy(l => l.ForOrdering).ToList();
        }
        public string LookupName(string lang, int? _valueID)
        {
            return db.LookupValues.Where(l => l.IsActive == true && l.ValueId == _valueID).Select(l => lang == "prs" ? l.DrName : lang == "ps" ? l.PaName : l.EnName).FirstOrDefault();
        }

        public RecordUserInfo RecordInfo(string table, string pk, long id)
        {
            var query = @"select ru.username InsertUser , ru.DisplayName InsertName , 
            eu.username UpdateUser , eu.DisplayName UpdateName ,
            tbl.InsertedBy , tbl.InsertedDate , tbl.LastUpdatedBy , tbl.LastUpdatedDate
            from {0} tbl 
            left join AspNetUsers ru on tbl.InsertedBy = ru.Id
            left join AspNetUsers eu on tbl.LastUpdatedBy = eu.Id
            where tbl.{1} = @p0";

            query = string.Format(query, table, pk);

            return db.Database.SqlQuery<RecordUserInfo>(query, id).SingleOrDefault();
        }

        /*************************************************
        ***************LOOKUP MGT FUNCS*******************
        *************************************************/

        public List<LookupValue> SearchLookupValue(string LookupCode, string ValueName, string Lang)
        {
            return db.Database.SqlQuery<LookupValue>("LookupValue_List @p0, @p1, @p2", LookupCode, ValueName, Lang).ToList();
        }


        public List<ModuleVM> Modules_List(string Lang)
        {
            return db.Database.SqlQuery<ModuleVM>("Modules_List @p0", Lang).ToList();
        }
    }
}