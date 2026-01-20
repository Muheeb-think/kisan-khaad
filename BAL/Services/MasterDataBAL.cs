using DAL.MasterData;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public interface IMasterDataBAL
    {
        public List<Village> GetVillageList(int? block);
        public RegisterViewModel SelectTehsilandFarmerCateogryList();
        public List<BlockModel> SelectBlockList(int? Tehsil);
        public List<CropModel> SelectCropList();
        public List<SeasonModel> SelectSeasonList();
        public List<FertilizerModel> SelectFertilizerList();
        public List<FertilizerCropMappingModel> fertilizerCropMappingModelsList();
        public List<TehsilModel> SelectTehsilList();
    }

    public class MasterDataBAL : IMasterDataBAL
    {
        private readonly IMastersData _mastersData;
        public MasterDataBAL(IMastersData mastersData)
        {
            this._mastersData = mastersData;
        }
        public List<Village> GetVillageList(int? block)
        {
            DataTable dt = _mastersData.SelectVillageMaster(block);
            List<Village> villageList = new List<Village>();

            foreach (DataRow row in dt.Rows)
            {
                villageList.Add(new Village
                {
                    Id = Convert.ToInt32(row["Id"]),
                    BlockId = Convert.ToInt32(row["BlockId"]),
                    BlockName = row["BlockName"].ToString(),
                    VillageNameHi = row["VillageNameHi"].ToString(),
                    VillageNameEn = row["VillageNameEng"].ToString()
                });
            }
            return villageList;
        }
        public List<BlockModel> SelectBlockList(int? tahsilId)
        {
            DataTable dt = _mastersData.SelectBlock(tahsilId);
            var blockList = new List<BlockModel>();

            foreach (DataRow row in dt.Rows)
            {
                blockList.Add(new BlockModel
                {
                    BlockId = Convert.ToInt32(row["BlockId"]),
                    BlockName = row["BlockName"].ToString()
                });
            }
            return blockList;
        }
        public RegisterViewModel SelectTehsilandFarmerCateogryList()
        {
            DataSet ds = _mastersData.SelectTehsil();

            RegisterViewModel model = new RegisterViewModel
            {
                TehsilList = new List<TehsilModel>(),
                FarmerCategoryList = new List<FarmerCategoryModel>(),
                DOB = null
            };

            if (ds != null && ds.Tables.Count >= 2)
            {
                // Tehsil List
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    model.TehsilList.Add(new TehsilModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        TehsilName = row["TehsilName"]?.ToString()
                    });
                }

                // Farmer Category List
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    model.FarmerCategoryList.Add(new FarmerCategoryModel
                    {
                        CategoryId = Convert.ToInt32(row["CategoryId"]),
                        CategoryNameHindi = row["CategoryNameHindi"]?.ToString()
                    });
                }
            }

            return model;
        }
        public List<CropModel> SelectCropList()
        {
            DataTable dt = _mastersData.SelectCropMaster();
            List<CropModel> cropList = new List<CropModel>();

            foreach (DataRow row in dt.Rows)
            {
                cropList.Add(new CropModel
                {
                    CropId = Convert.ToInt32(row["CropId"]),
                    SeasonId = Convert.ToInt32(row["SeasonId"]),
                    SeasonNameHindi = row["SeasonNameHindi"].ToString(),
                    CropNameHindi = row["CropNameHindi"].ToString(),
                });
            }
            return cropList;
        }
        public List<SeasonModel> SelectSeasonList()
        {
            DataTable dt = _mastersData.SelectSeasonMaster();
            List<SeasonModel> seasonList = new List<SeasonModel>();

            foreach (DataRow row in dt.Rows)
            {
                seasonList.Add(new SeasonModel
                {
                    SeasonId = Convert.ToInt32(row["SeasonId"]),
                    SeasonNameHindi = row["SeasonNameHindi"].ToString(),
                });
            }
            return seasonList;
        }

        public List<FertilizerModel> SelectFertilizerList()
        {
            DataTable dt = _mastersData.SelectFertilizerMaster();
            List<FertilizerModel> fertilizers = new List<FertilizerModel>();

            foreach (DataRow row in dt.Rows)
            {
                fertilizers.Add(new FertilizerModel
                {
                    FertilizerId = Convert.ToInt32(row["FertilizerId"]),
                    FertilizerNameHindi = Convert.ToString(row["FertilizerNameHindi"]),
                    FertilizerNameEnglish = Convert.ToString(row["FertilizerNameEnglish"]),
                    FertilizerRate = Convert.ToString(row["FertilizerRate"])
                });
            }
            return fertilizers;
        }

        public List<FertilizerCropMappingModel> fertilizerCropMappingModelsList()
        {
            DataTable dt = _mastersData.SelectCropFertilizerMapping();
            List<FertilizerCropMappingModel> fertilizerCrops = new List<FertilizerCropMappingModel>();

            foreach (DataRow row in dt.Rows)
            {
                fertilizerCrops.Add(new FertilizerCropMappingModel
                {
                    FertilizerId = Convert.ToInt32(row["FertilizerId"]),
                    FertilizerNameEnglish = Convert.ToString(row["FertilizerNameEnglish"]),
                    FertilizerNameHindi = Convert.ToString(row["FertilizerNameHindi"]),
                    Dose_per_hectare_kg = Convert.ToString(row["Dose_per_hectare_kg"]),
                    CropNameHindi = Convert.ToString(row["CropNameHindi"]),

                });
            }
            return fertilizerCrops;
        }

        public List<TehsilModel> SelectTehsilList()
        {
            DataSet ds = _mastersData.SelectTehsil();
            List<TehsilModel> tehsilModels = new List<TehsilModel>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tehsilModels.Add(new TehsilModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    TehsilName = row["TehsilName"]?.ToString()
                });
            }
            return tehsilModels;
        }

    }
}
