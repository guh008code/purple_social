using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;

using System.IO;
using System.Web.UI;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

using System.Globalization;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public class Util : System.Web.UI.Page
{
    #region Valida CNPJ
    public bool Valida_CNPJ(string strCNPJ)
    {
        string CNPJ = strCNPJ.Replace(".", "");
        CNPJ = CNPJ.Replace("/", "");
        CNPJ = CNPJ.Replace("-", "");
        int[] digitos, soma, resultado;
        int nrDig;
        string ftmt;
        bool[] CNPJOk;
        ftmt = "6543298765432";
        digitos = new int[14];
        soma = new int[2];
        soma[0] = 0;
        soma[1] = 0;
        resultado = new int[2];
        resultado[0] = 0;
        resultado[1] = 0;
        CNPJOk = new bool[2];
        CNPJOk[0] = false;
        CNPJOk[1] = false;
        try
        {
            for (nrDig = 0; nrDig < 14; nrDig++)
            {
                digitos[nrDig] = int.Parse(
                CNPJ.Substring(nrDig, 1));
                if (nrDig <= 11)
                    soma[0] += (digitos[nrDig] *
                    int.Parse(ftmt.Substring(
                    nrDig + 1, 1)));
                if (nrDig <= 12)
                    soma[1] += (digitos[nrDig] *
                    int.Parse(ftmt.Substring(
                    nrDig, 1)));
            }
            for (nrDig = 0; nrDig < 2; nrDig++)
            {
                resultado[nrDig] = (soma[nrDig] % 11);
                if ((resultado[nrDig] == 0) || (
                resultado[nrDig] == 1))
                    CNPJOk[nrDig] = (
                    digitos[12 + nrDig] == 0);
                else
                    CNPJOk[nrDig] = (
                    digitos[12 + nrDig] == (
                    11 - resultado[nrDig]));
            }
            return (CNPJOk[0] && CNPJOk[1]);
        }
        catch
        {
            return false;
        }
    }
    #endregion

    public bool IsCpf(string cpf)
    {
        cpf = cpf.Replace("-", "");
        cpf = cpf.Replace(".", "");
        cpf = cpf.Replace("/", "");

        Regex reg = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
        if (!reg.IsMatch(cpf))
            return false;

        int d1, d2;
        int soma = 0;
        string digitado = "";
        string calculado = "";

        // Pesos para calcular o primeiro digito
        int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        // Pesos para calcular o segundo digito
        int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] n = new int[11];

        bool retorno = false;

        // Limpa a string
        cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

        // Se o tamanho for < 11 entao retorna como inválido
        if (cpf.Length != 11)
            return false;

        // Caso coloque todos os numeros iguais
        switch (cpf)
        {
            case "11111111111":
                return false;
            case "00000000000":
                return false;
            case "2222222222":
                return false;
            case "33333333333":
                return false;
            case "44444444444":
                return false;
            case "55555555555":
                return false;
            case "66666666666":
                return false;
            case "77777777777":
                return false;
            case "88888888888":
                return false;
            case "99999999999":
                return false;
        }

        try
        {
            // Quebra cada digito do CPF
            n[0] = (int)Convert.ToInt64(cpf.Substring(0, 1));
            n[1] = (int)Convert.ToInt64(cpf.Substring(1, 1));
            n[2] = (int)Convert.ToInt64(cpf.Substring(2, 1));
            n[3] = (int)Convert.ToInt64(cpf.Substring(3, 1));
            n[4] = (int)Convert.ToInt64(cpf.Substring(4, 1));
            n[5] = (int)Convert.ToInt64(cpf.Substring(5, 1));
            n[6] = (int)Convert.ToInt64(cpf.Substring(6, 1));
            n[7] = (int)Convert.ToInt64(cpf.Substring(7, 1));
            n[8] = (int)Convert.ToInt64(cpf.Substring(8, 1));
            n[9] = (int)Convert.ToInt64(cpf.Substring(9, 1));
            n[10] = (int)Convert.ToInt64(cpf.Substring(10, 1));
        }
        catch
        {
            return false;
        }

        // Calcula cada digito com seu respectivo peso
        for (int i = 0; i <= peso1.GetUpperBound(0); i++)
            soma += (peso1[i] * (int)Convert.ToInt64(n[i]));

        // Pega o resto da divisao
        int resto = soma % 11;

        if (resto == 1 || resto == 0)
            d1 = 0;
        else
            d1 = 11 - resto;

        soma = 0;

        // Calcula cada digito com seu respectivo peso
        for (int i = 0; i <= peso2.GetUpperBound(0); i++)
            soma += (peso2[i] * (int)Convert.ToInt64(n[i]));

        // Pega o resto da divisao
        resto = soma % 11;

        if (resto == 1 || resto == 0)
            d2 = 0;
        else
            d2 = 11 - resto;

        calculado = d1.ToString() + d2.ToString();
        digitado = n[9].ToString() + n[10].ToString();

        // Se os ultimos dois digitos calculados bater com
        // os dois ultimos digitos do cpf entao é válido
        if (calculado == digitado)
            retorno = true;
        else
            retorno = false;

        return retorno;
    }

    #region Valida email
    public bool Valida_Email(string strEmail)
    {
        try
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(strEmail))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            throw;
        }
    }
    #endregion

    public long ReturnMegaBytes(int Byte)
    {
        long valor = Byte * 1024;
        valor = valor * 1024;
        return valor;
    }

    public string removerAcentos(string texto)
    {
        string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

        for (int i = 0; i < comAcentos.Length; i++)
        {
            texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
        }
        return texto;
    }

    public string removerLetras(string texto)
    {
        string antigo = texto;
        texto = "";

        for (int i = 0; i < antigo.Length; i++)
        {
            if (antigo[i].ToString() == "1" || antigo[i].ToString() == "2" || antigo[i].ToString() == "3" || antigo[i].ToString() == "4" ||
                antigo[i].ToString() == "5" || antigo[i].ToString() == "6" || antigo[i].ToString() == "7" || antigo[i].ToString() == "8" ||
                antigo[i].ToString() == "9" || antigo[i].ToString() == "0")
            {
                texto = texto + antigo[i].ToString();
            }
            else
            {
                texto = texto + " ";
            }
        }
        return texto;
    }

    public string removerNumeros(string texto)
    {
        string antigo = texto;
        texto = "";

        for (int i = 0; i < antigo.Length; i++)
        {
            if (antigo[i].ToString() != "1" && antigo[i].ToString() != "2" && antigo[i].ToString() != "3" && antigo[i].ToString() != "4" &&
                antigo[i].ToString() != "5" && antigo[i].ToString() != "6" && antigo[i].ToString() != "7" && antigo[i].ToString() != "8" &&
                antigo[i].ToString() != "9" && antigo[i].ToString() != "0")
            {
                texto = texto + antigo[i].ToString();
            }
            else
            {
                texto = texto + " ";
            }
        }
        return texto;
    }

    public bool IsNumeric(string data)
    {
        bool isnumeric = false;
        char[] datachars = data.ToCharArray();

        foreach (var datachar in datachars)
            isnumeric = isnumeric ? char.IsDigit(datachar) : isnumeric;


        return isnumeric;
    }

    public bool TemNumeros(string data)
    {
        bool isnumeric = false;
        char[] datachars = data.ToCharArray();

        foreach (var datachar in datachars)
        {
            if (char.IsDigit(datachar))
            {
                isnumeric = true;
                break;
            }
        }

        return isnumeric;
    }

    public bool TemPalavras(string data)
    {
        bool ispalavra = false;
        char[] datachars = data.ToCharArray();

        foreach (var datachar in datachars)
        {
            if (char.IsLetter(datachar))
            {
                ispalavra = true;
                break;
            }
        }


        return ispalavra;
    }

    public Boolean checkForSQLInjection(string userInput)
    {
        bool isSQLInjection = false;
        string[] sqlCheckList = { "<",
                                      ">",
                                      "<!--",
                                      "--",
                                       ";--",
                                       ";",
                                       "/*",
                                       "*/",
                                        "@@",
                                       "nchar",
                                       "varchar",
                                       "nvarchar",
                                       "begin",
                                       "create",
                                       "cursor",
                                       "declare",
                                       "delete",
                                       "drop",
                                       "execute",
                                       "fetch",
                                            "insert",
                                          "kill",
                                             "select",
                                           "sys",
                                            "sysobjects",
                                            "syscolumns",
                                           "table",
                                           "update"
                                       };

        string CheckString = userInput.Replace("'", "''");
        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
        {
            if (userInput.Contains(sqlCheckList[i]))
            {
                isSQLInjection = true;
            }

            //if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
            //{
            //    isSQLInjection = true;
            //}
        }
        return isSQLInjection;
    }

    public string checkForSQLInjectionString(string userInput)
    {
        string[] sqlCheckList = { "<",
                                      ">",
                                      "<!--",
                                       ";--",
                                       ";",
                                       "/*",
                                       "*/",
                                        "@@",
                                        "char",
                                       "nchar",
                                       "varchar",
                                       "nvarchar",
                                       "alter",
                                       "begin",
                                       "cast",
                                       "create",
                                       "cursor",
                                       "declare",
                                       "delete",
                                       "drop",
                                       "end",
                                       "exec",
                                       "execute",
                                       "fetch",
                                        "insert",
                                        "kill",
                                        "select",
                                        "sys",
                                        "sysobjects",
                                        "syscolumns",
                                        "table",
                                        "update"
                                       };

        string CheckString = userInput.Replace("'", "''");
        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
        {
            userInput = userInput.Replace(sqlCheckList[i], "");
        }
        return userInput;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="entrada"></param>
    /// <returns></returns>
    public static string Criptografar(string entrada)
    {
        TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

        try
        {
            if (entrada.Trim() != "")
            {

                string myKey = "1111111111111111";   //Aqui vc inclui uma chave qualquer para servir de base para cifrar, que deve ser a mesma no método Decodificar
                tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
                tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                //tripledescryptoserviceprovider.Padding = PaddingMode.Zeros;
                ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateEncryptor();
                ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
                byte[] buff = Encoding.ASCII.GetBytes(entrada);

                return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
            }
            else
            {
                return "";
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            tripledescryptoserviceprovider = null;
            md5cryptoserviceprovider = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entrada"></param>
    /// <returns></returns>
    public static string Descriptografar(string entrada)
    {
        TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

        try
        {
            if (entrada.Trim() != "")
            {
                string myKey = "1111111111111111";  //Aqui vc inclui uma chave qualquer para servir de base para cifrar, que deve ser a mesma no método Codificar
                tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
                tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                //tripledescryptoserviceprovider.V.Padding = PaddingMode.Zeros;
                ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(entrada);

                return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

            }
            else
            {
                return "";
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            tripledescryptoserviceprovider = null;
            md5cryptoserviceprovider = null;
        }
    }

    public static void Resize(string srcPath, string destPath, int nWidth, int nHeight)
    {
        bool vertical = false;
        string temp;
        // abre arquivo original
        System.Drawing.Image img = System.Drawing.Image.FromFile(srcPath);
        int oWidth = img.Width; // largura original
        int oHeight = img.Height; // altura original

        // redimensiona se necessario
        if (oWidth > nWidth || oHeight > nHeight)
        {
            if (oWidth >= oHeight)
            {
                // imagem horizontal
                oHeight = (oHeight * nWidth) / oWidth;
                oWidth = 150;
            }
            else
            {
                // imagem vertical
                oWidth = (oWidth * nHeight) / oHeight;
                oWidth = img.Width;
                oHeight = img.Height;
                vertical = false;
            }
        }

        if (!vertical)
        {
            // cria a copia da imagem
            System.Drawing.Image imgThumb = img.GetThumbnailImage(oWidth, oHeight, null, new System.IntPtr(0));

            if (srcPath == destPath)
            {
                temp = destPath + ".tmp";
                imgThumb.Save(temp, ImageFormat.Jpeg);
                img.Dispose();
                imgThumb.Dispose();
                File.Delete(srcPath); // deleta arquivo original
                File.Copy(temp, srcPath); // copia a nova imagem
                File.Delete(temp); // deleta temporário
            }
            else
            {
                imgThumb.Save(destPath, ImageFormat.Jpeg); // salva nova imagem no destino
                imgThumb.Dispose(); // libera memoria
                img.Dispose(); // libera memória
            }
        }
        else
        {
            img.Dispose(); // libera memória
        }
    }

    public static void GenerateThumbImage(string originalImagePath, string thumbImagePath, int newWidth, int newHeight)
    {
        Bitmap srcBmp = new Bitmap(originalImagePath);
        float ratio = 1;
        float minSize = Math.Min(newHeight, newHeight);

        if (srcBmp.Width > srcBmp.Height)
        {
            ratio = minSize / (float)srcBmp.Width;
        }
        else
        {
            minSize = 350;
            ratio = minSize / (float)srcBmp.Height;
        }

        SizeF newSize = new SizeF(srcBmp.Width * ratio, srcBmp.Height * ratio);
        Bitmap target = new Bitmap((int)newSize.Width, (int)newSize.Height);

        using (Graphics graphics = Graphics.FromImage(target))
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(srcBmp, 0, 0, newSize.Width, newSize.Height);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                target.Save(thumbImagePath);
                srcBmp.Dispose();
                File.Delete(originalImagePath);
                target.Dispose();
            }
        }
    }
}


