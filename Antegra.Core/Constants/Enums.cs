using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Constants
{
    public class Enums
    {
        public const string SecretKey = "9fbac4553b7548e6ad7401f201056083";

    

        public enum JobScheduleTimeType
        {
            [Display(Description = "Dakika")]
            Minute = 1,
            [Display(Description = "Saat")]
            Hour = 2,
            [Display(Description = "Gün")]
            Day = 3,
        }
        public enum UserType
        {
            [Display(Description = "Firma")]
            Firm = 1,
            [Display(Description = "Kişisel")]
            Personal = 2,

        }
        public enum CompanyPropertyKind
        {
            [Display(Description = "Fiyatlandırma")]
            Price = 1,
            [Display(Description = "Teknik ve Lokasyon Özellikleri")]
            TechAndLocation = 2,
            [Display(Description = "Hizmet ve Organizasyon")]
            Organization = 3,
            [Display(Description = "Kapasite Bilgileri")]
            Capacity = 4,
            [Display(Description = "Genel Özellikler")]
            General = 5,
            [Display(Description = "Sık Sorulan Sorular")]
            Faq = 5,
            [Display(Description = "Hakkinda")]
            About = 6,
        }
        public enum CompanyPropertyValueType
        {
            [Display(Description = "Yazı")]
            String = 1,
            [Display(Description = "Sayısal")]
            Integer = 2,
            [Display(Description = "Para")]
            Money = 3,
            [Display(Description = "Var/Yok")]
            Bool = 4,
        }





        public const string Admin = "Admin";
        public const string User = "Kullanici";


    }


    public static class EnumDisplay
    {

        public static string GetDisiplayDescription(this Enum enm)
        {

            var das = enm;
            var enumType = enm.GetType().GetMember(enm.ToString());
            try
            {
                return enumType.FirstOrDefault().GetCustomAttribute<DisplayAttribute>()?.Description;
            }
            catch (Exception)
            {

                return "";
            }




        }

    }

}
