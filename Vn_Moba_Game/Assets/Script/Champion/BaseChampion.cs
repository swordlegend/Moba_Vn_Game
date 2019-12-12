using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChampion
{
    public int Id { get; set; }
    public string Ten { get; set; }
    public string ThongTin { get; set; }
    public Texture2D AnhDaiDien { get; set; }

    // Thuộc tính
    public int SatThuongTay { get; set; }
    public int SatThuongPhep { get; set; }
    public int Giap { get; set; }
    public int KhangPhep { get; set; }
    public float TocDanh { get; set; } // max 2.5
    public int TiLeChiMang { get; set; } // % max 100%
    public int HoiChieu { get; set; } // % max 100%
    public float HoiMau { get; set; }
    public float HoiNangLuong { get; set; }
    public int TocChay { get; set; }

    // Prefab
    public GameObject prefabModel { get; set; }
}