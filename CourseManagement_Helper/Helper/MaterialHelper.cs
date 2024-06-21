using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class MaterialHelper
    {
        public static List<MaterialModel> ConvertMaterialListToMaterialModelList(List<Material> list)
        {
            try
            {
                List<MaterialModel> materialModelList = new List<MaterialModel>();
                if(list != null)
                {
                   foreach(var item in list)
                    {
                        MaterialModel materialModel = new MaterialModel();
                        materialModel.MaterialId = item.MaterialId;
                        materialModel.Title = item.Title;
                        materialModel.FilePath = item.FilePath;
                        materialModel.CourseId = (int)item.CourseId;
                        materialModel.Files = item.FilePath.Split(',');
                        materialModel.CourseTitle = item.Course.Title;
                        materialModelList.Add(materialModel);
                    }
                }
                return materialModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
