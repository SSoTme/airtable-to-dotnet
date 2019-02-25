using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;

namespace airtabletodotnet.Lib.DataClasses
{                            
    public partial class CarModel
    {
        private void InitPoco()
        {
            
            
                this.ModelYears = new BindingList<ModelYear>();
            

        }
        
        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();

        

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CarModelId")]
        public String CarModelId { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "createdTime")]
        public Nullable<DateTime> createdTime { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Item")]
        public Nullable<Int32> Item { get; set; }
    
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }
    

        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ModelYears")]
        public BindingList<ModelYear> ModelYears { get; set; }
            
        /// <summary>
        /// Find the related ModelYears (from the list provided) and attach them locally to the ModelYears list.
        /// </summary>
        public void LoadModelYears(IEnumerable<ModelYear> modelYears)
        {
            modelYears.Where(whereModelYear => whereModelYear.CarModel == this.CarModelId)
                    .ToList()
                    .ForEach(feModelYear => this.ModelYears.Add(feModelYear));
        }
        

        
        
        private static string CreateCarModelWhere(IEnumerable<CarModel> carModels, String forignKeyFieldName = "CarModelId")
        {
            if (!carModels.Any()) return "1=1";
            else 
            {
                var idList = carModels.Select(selectCarModel => String.Format("'{0}'", selectCarModel.CarModelId));
                var csIdList = String.Join(",", idList);
                return String.Format("{0} in ({1})", forignKeyFieldName, csIdList);
            }
        }
        
    }
}
