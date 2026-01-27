using DAL.SqlHeplers;
using DAL.ViewModel;
using Jalaun.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace DAL.MasterData
{
    public interface IMastersData
    {

        public DataSet SelectTehsil();
        public DataTable SelectBlock(int? Tehsil);
        public DataTable SelectVillageMaster(int? blockId);
        public int SaveTehsil(TehsilViewModel model);
        public int SaveSociety(SocietyViewModel model);
        public DataTable GetSociety(int? id);


        public DataTable SelectCropMaster();
        public DataTable SelectSeasonMaster();
        public DataTable SelectFertilizerMaster();

        public DataTable SelectCropFertilizerMapping();

        public int InsertVillage(VillageViewMaster obj);
        public int VillageUpdate(VillageViewMaster obj);
        public int VillageDelete(VillageViewMaster obj);
        public int InsertUpdateCropMaster(CropViewModel obj);
        public int CropDelete(CropViewModel obj);
        public int FertilizerDelete(FertilizerViewModel obj);
        public int FertilizerInsertUpdate(FertilizerViewModel obj);

        public int InsertUpdateCropFertilizerMapping(FertilizerCropMappingViewModel obj);
        public int DeleteFertilizerMapping(FertilizerCropMappingViewModel obj);
        public int CreateUserRole(RoleViewModel model);
        public DataTable RoleList(int? RoleId);
        public int CreateUser(UserViewModel model);
        public DataTable GetUser(int? Id);
        public DataTable GetUserRolesAndPerMissionModule(int? RoleId);
        public int SaveUserRolesAndPermissionModule(List<UserModulesDetails> permission);
        public DataTable GetVillageByTehsil(int? tehsilid);
        public DataSet GetSocietyDashboardData(long societyid);
        public int SaveFertilizerStock(FertilizerStockVM model, int userId);
        public void DistributeFertilizer(DistributeVM model, int userId);

    }
    public class MasterData : IMastersData
    {
        private readonly IDBHelpers _dataAccess;
        public MasterData(IDBHelpers dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        public int InsertVillage(VillageViewMaster obj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BlockId",obj.BlockId),
                new SqlParameter("@VillageNameHindi",obj.VillageNameHindi),
                new SqlParameter("@UserId","1")
            };
            int result = _dataAccess.ExecuteInsert("InsertVillage", parameters);
            return result;
        }
        public DataTable SelectBlock(int? Tehsil)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@Tehsil",Tehsil)
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SelectBlock", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataSet SelectTehsil()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                { };
                DataSet ds = _dataAccess.ExecuteDataSet("SelectTehsilMaster", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable SelectVillageMaster(int? blockId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@BlockId",blockId ?? 0)
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SelectVillageMaster", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable SelectCropMaster()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SelectCrop", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable SelectSeasonMaster()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SeasonMaster", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from dal:" + ex.Message);
            }
        }
        public DataTable SelectCropFertilizerMapping()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SP_SelectCropFertilizerMapping", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from dal:" + ex.Message);
            }
        }

        public DataTable SelectFertilizerMaster()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SP_SelectFertilizerMaster", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from dal:" + ex.Message);
            }
        }
        public int InsertUpdateCropMaster(CropViewModel obj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CropNameHindi",obj.CropNameHindi),
                new SqlParameter("@SeasonId",obj.SeasonId),
                new SqlParameter("@CropId",obj.CropId > 0 ? obj.CropId : 0),
                new SqlParameter("@UserId","1")
            };
            int result = _dataAccess.ExecuteInsert("SP_InsertUpdateCropDetails", parameters);
            return result;
        }
        public int VillageUpdate(VillageViewMaster village)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@VillageId",village.Id),
                    new SqlParameter("@VillageName",village.VillageNameHindi),
                    new SqlParameter("@UserId",1),//User perform action 
                    new SqlParameter("@action",1)
                };
                int result = _dataAccess.ExecuteInsert("VillageEditDelete", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int VillageDelete(VillageViewMaster village)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@VillageId",village.Id),
                    new SqlParameter("@VillageName",village.VillageNameHindi),
                    new SqlParameter("@UserId",1), //User perform action 
                    new SqlParameter("@action",2)
                };
                int result = _dataAccess.ExecuteInsert("VillageEditDelete", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int CropDelete(CropViewModel obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CropId",obj.CropId),
                    new SqlParameter("@UserId","1"),
                };
                int result = _dataAccess.ExecuteInsert("SP_DeleteDropDetails", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int FertilizerDelete(FertilizerViewModel obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FertilizerId",obj.FertilizerId),
                    new SqlParameter("@UserId","1"),
                };
                int result = _dataAccess.ExecuteInsert("SP_DeleteFertilizerMaster", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }

        public int FertilizerInsertUpdate(FertilizerViewModel obj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FertilizerId",obj.FertilizerId > 0 ? obj.FertilizerId : 0),
                new SqlParameter("@FertilizerNameHindi",obj.FertilizerNameHindi),
                new SqlParameter("@FertilizerNameEnglish",obj.FertilizerNameEnglish),
                new SqlParameter("@FertilizerRate",obj.FertilizerRate),
                new SqlParameter("@UserId","1")
            };
            int result = _dataAccess.ExecuteInsert("SP_InsertUpdateFertilizerMaster", parameters);
            return result;
        }

        public int DeleteFertilizerMapping(FertilizerCropMappingViewModel obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MappingId",obj.MappingId),
                    new SqlParameter("@UserId","1"),
                };
                int result = _dataAccess.ExecuteInsert("SP_DeleteCropFertilizerMapping", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }

        public int InsertUpdateCropFertilizerMapping(FertilizerCropMappingViewModel obj)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MappingId",obj.MappingId > 0 ? obj.MappingId : 0),
                new SqlParameter("@FertilizerId",obj.FertilizerId > 0 ? obj.FertilizerId : 0),
                new SqlParameter("@CropId",obj.CropId),
                new SqlParameter("@DosePerHectareKg",obj.Dose_per_hectare_kg),
                new SqlParameter("@UserId","1"),
            };
            int result = _dataAccess.ExecuteInsert("SP_InsertUpdateCropFertilizerMapping", parameters);
            return result;
        }
        public DataTable GetVillageByTehsil(int? tehsilid)
        {
            SqlParameter[] parameters = new SqlParameter[]
         {

         new SqlParameter("@Action","GetVillegeByTehsil"),
         new SqlParameter("@GetById",tehsilid > 0 ? tehsilid : 0),
         };
            DataTable dt = _dataAccess.ExecuteDataTable("Sp_BindDDL", parameters);
            return dt;
        }

        #region Muheeb
        public int SaveTehsil(TehsilViewModel model)
        {
            try
            {

                SqlParameter[] parameters =
            {
                     new SqlParameter("@Id", model.Id>0 ? model.Id: (object)DBNull.Value),
                    new SqlParameter("@TehsilNameEng", model.TehsilNameEng),
                    new SqlParameter("@TehsilNameHin", model.TehsilNameHi),
                    new SqlParameter("@BlockId", model.BlockId),
                    new SqlParameter("@CreatedBy", 1)
                };
                var result = _dataAccess.ExecuteNonQuery("InsertUpdateTehsil", commandType: CommandType.StoredProcedure, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int SaveSociety(SocietyViewModel model)
        {
            try
            {

                SqlParameter[] parameters =
            {
                     new SqlParameter("@Id", model.Id>0 ? model.Id: (object)DBNull.Value),
                    new SqlParameter("@SocietyNameEng", model.SocietyNameEng),
                    new SqlParameter("@SocietyNameHi", model.SocietyNameHi),
                    new SqlParameter("@TehsilId", model.TehsilId),
                       new SqlParameter("@BlockId", model.BlockId),
                     new SqlParameter("@VillageId", model.VillageId),
                      new SqlParameter("@Mobile", model.Mobile),
                       new SqlParameter("@Email", model.Email),
                    new SqlParameter("@CreatedBy",model.CreatedBy)
                };
                var result = _dataAccess.ExecuteNonQuery("sp_InsertUpdateSociety", commandType: CommandType.StoredProcedure, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable GetSociety(int? id)
        {
            try
            {

                SqlParameter[] parameters =
            {
                     new SqlParameter("@Id", id>0 ? id: (object)DBNull.Value),

                };
                var result = _dataAccess.ExecuteDataTable("sp_GetSociety", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int CreateUserRole(RoleViewModel model)
        {
            try
            {

                SqlParameter[] parameters =
            {
                     new SqlParameter("@RoleTypeId", model.RoleTypeId),
                     new SqlParameter("@RoleName", model.RoleName),
                     new SqlParameter("@RoleNameHi", model.RoleNameHi),
                     new SqlParameter("@IsActive", model.IsActive)


                };
                var result = _dataAccess.ExecuteNonQuery("sp_RoleSaveUpdate", commandType: CommandType.StoredProcedure, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable RoleList(int? RoleId)
        {
            try
            {

                SqlParameter[] parameters =
                {
                     new SqlParameter("@RoleId",RoleId),


                };
                var result = _dataAccess.ExecuteDataTable("sp_RoleList", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int CreateUser(UserViewModel model)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleTypeId", model.RoleTypeId),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Mobile", model.mobile),
                 new SqlParameter("@Password", model.Password),
                new SqlParameter("@ConfirmPassword", model.ConfirmPassword),
                new SqlParameter("@IsActive", model.IsActive),
                new SqlParameter("@UserNumber", model.UserNumber ?? (object)DBNull.Value)
                };
                var result = _dataAccess.ExecuteNonQuery("sp_SaveUpdateUserlogin", commandType: CommandType.StoredProcedure, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable GetUser(int? Id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserNumber", Id),

                };
                var result = _dataAccess.ExecuteDataTable("sp_getUsers", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public DataTable GetUserRolesAndPerMissionModule(int? RoleId)
        {
            try
            {

                SqlParameter[] parameters =
                {
                     new SqlParameter("@RoleId",RoleId),
                };
                var result = _dataAccess.ExecuteDataTable("sp_getModuleWithPermission", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }

        public int SaveUserRolesAndPermissionModule(List<UserModulesDetails> permission)
        {
            int result = 0;
            try
            {
                foreach (var item in permission)
                {
                    SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleId",item.RoleTypeId),
                    new SqlParameter("@MainMenuId",item.MainMenuId),
                     new SqlParameter("@SubMenuId",item.SubMenuId),
                    new SqlParameter("@CanAccess",item.CanAccess),
                };
                    result = _dataAccess.ExecuteNonQuery("sp_SaveUpdateRolePermission", commandType: CommandType.StoredProcedure, parameters);

                }
            }

            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
            return result;
        }

        public DataSet GetSocietyDashboardData(long societyid)
        {
            try
            {

                SqlParameter[] parameters =
                {
                     new SqlParameter("@MobileNo",societyid),
                };
                var result = _dataAccess.ExecuteDataSet("sp_getSocietyFertilizerDetails", parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
        public int SaveFertilizerStock(FertilizerStockVM model, int userId)
        {
            int result = 0;
            try
            {
                model.SocietyId = userId;
                SqlParameter[] parameters =
                    {
                new SqlParameter("@StockID", model.StockID),
                new SqlParameter("@FertilizerID", model.FertilizerID),
                new SqlParameter("@OpeningStock", (object)model.OpeningStock ?? DBNull.Value),
                new SqlParameter("@PurchasedQty", (object)model.PurchasedQty ?? DBNull.Value),
                new SqlParameter("@UsedQty", (object)model.UsedQty ?? DBNull.Value),
                new SqlParameter("@QtyUnit", model.QtyUnit),
                new SqlParameter("@SocietyId", model.SocietyId),
                new SqlParameter("@Remarks", model.Remarks),
                new SqlParameter("@UserId", userId)
            };

                result = _dataAccess.ExecuteNonQuery("sp_SaveUpdateGetFertilizerStock", commandType: CommandType.StoredProcedure, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public void DistributeFertilizer(DistributeVM model, int userId)
        {
            SqlParameter[] parameters =
                   {
            new SqlParameter("@DemandDetailsId", model.DemandDetailsId),
            new SqlParameter("@FertilizerId", model.FertilizerId),
            new SqlParameter("@DistributeQty", model.DistributeQty),
            new SqlParameter("@SocietyId", model.SocietyId),
            new SqlParameter("@UserId", userId),
            new SqlParameter("@Remarks", model.Remarks),
        };
            var result = _dataAccess.ExecuteNonQuery("SP_DistributeFertilizerToFarmer", commandType: CommandType.StoredProcedure, parameters);

        }


        #endregion
    }
}
