﻿using System;
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

        public enum ContactType
        {
            [Display(Description = "Telefon")]
            Phone1 = 1,
            [Display(Description = "Telefon")]
            Phone2 = 6,
            [Display(Description = "Telefon")]
            Phone3 = 7,
            [Display(Description = "Telefon")]
            Phone4 = 8,
            [Display(Description = "İnstagram")]
            Instagram = 2,
            [Display(Description = "Youtube")]
            Youtube = 3,
            [Display(Description = "Linkedin")]
            Linkedin = 4,
            [Display(Description = "Facebook")]
            Facebook = 5,
            [Display(Description = "E-Posta")]
            Email = 9,
            [Display(Description = "Web Site")]
            WebSite = 10,
            [Display(Description = "Adres")]
            Address = 11,
            [Display(Description = "langitude")]
            Lang = 12,
            [Display(Description = "latitude")]
            lat = 13,
            [Display(Description = "Harita")]
            Map = 14,
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
            [Display(Description = "Liste")]
            List = 5,
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
