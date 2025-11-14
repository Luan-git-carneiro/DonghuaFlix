using System.ComponentModel;
using System.Linq;            // ToDictionary
using System.Reflection;      // GetType, GetMember, GetCustomAttributes
using System.Collections.Generic;


namespace DonghuaFlix.Backend.src.Core.Domain.Enum;

public enum Genre
    {
        [Description("Action")]
        ACTION = 1,
        [Description("Adventure")]
        ADVENTURE = 2,
        [Description("Avant Garde")]
        AVANT_GARDE = 3,
        [Description("Award Winning")]
        AWARD_WINNING = 4,
        [Description("Boys Love")]
        BOYS_LOVE = 5,
        [Description("Comedy")]
        COMEDY = 6,
        [Description("Drama")]
        DRAMA = 7,
        [Description("Fantasy")]
        FANTASY = 8,
        [Description("Girls Love")]
        GIRLS_LOVE = 9,
        [Description("Gourmet")]
        GOURMET = 10,
        [Description("Horror")]
        HORROR = 11,
        [Description("Mystery")]
        MYSTERY = 12,
        [Description("Romance")]
        ROMANCE = 13,
        [Description("Sci-Fi")]
        SCI_FI = 14,
        [Description("Slice of Life")]
        SLICE_OF_LIFE = 15,
        [Description("Sports")]
        SPORTS = 16,
        [Description("Supernatural")]
        SUPERNATURAL = 17,
        [Description("Suspense")]
        SUSPENSE = 18,
        // Adições específicas para donghua (animes chineses) comuns
        [Description("Wuxia")]          // Artes marciais históricas chinesas
        WUXIA = 19,
        [Description("Xianxia")]        // Imortais e cultivo de qi
        XIANXIA = 20,
        [Description("Xuanhuan")]       // Fantasia oriental com elementos modernos
        XUANHUAN = 21,
        [Description("Cultivation")]    // Foco em progressão de poder/cultivo
        CULTIVATION = 22,
        [Description("Historical Wuxia")] // Variação histórica de wuxia
        HISTORICAL_WUXIA = 23
    }

    public static class GenreExtensions
    {
        // Helper público para extrair atributos (agora com Reflection)
        public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Length > 0)
            {
                var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
                if (attributes.Length > 0)
                    return (T)attributes[0];
            }
            return null;
        }
       public static string ToStringValue(this Genre genre)
        {
            return genre.GetAttributeOfType<DescriptionAttribute>()?.Description ?? genre.ToString();
        }

        public static IEnumerable<Genre> AllGenres => Enum.GetValues<Genre>();

        public static Dictionary<string, Genre> StringToGenreMap => AllGenres.ToDictionary(g => g.ToStringValue(), g => g);
    }

