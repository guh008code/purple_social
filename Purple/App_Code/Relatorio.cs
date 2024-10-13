using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Relatorio
/// </summary>
public class RelatorioEnt
{

    public int? ID_USUARIO { get; set; }
    public string USUARIO { get; set; }
    public string email { get; set; }
    public int? quantidade_acesso { get; set; }
    public int? quantidade_amigos { get; set; }


}