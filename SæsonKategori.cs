using System;
using System.Collections.Generic;

public class SæsonKategori {
    private int SæsonKategoriId;
    public string Kategori { get; set; }  // super, høj, mellem, lav
    public int PrisMultiplikator { get; set; }
    public List<int> UgeNumre { get; set; } = new List<int>();
    
    public SæsonKategori() {
        UgeNumre = new List<int>();
    }
}