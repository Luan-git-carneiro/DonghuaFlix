namespace DonghuaFlix.Backend.src.Core.Domain.Enum;


[Flags]
public enum Genre
{
    Wuxia = 1 << 0,
    Xianxia = 1 << 1,
    Xuanhuan = 1 << 2,
    SciFi = 1 << 3,
    Romance = 1 << 4,
    Acao = 1 << 5,
    Misterio = 1 << 6,
    Comedia = 1 << 7,
    Historico = 1 << 8,
    Josei = 1 << 9,
    Isekai = 1 << 10,
    Mecha = 1 << 11,
    Horror = 1 << 12,
    SliceOfLife = 1 << 13,
    Sports = 1 << 14,
}
