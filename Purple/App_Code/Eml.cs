using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;

public class Eml
{
    public void EnviarEmail(string sRemetente, string sListaDestinatario, string sListaCopia, string sListaCopiaOculta, bool bFormatoHTML, string sTitulo, string sAssunto, string sListaCaminhoAnexo, string sServer, bool bSSL, int iPortaSmtp, string sUsuario, string sSenha)
    {
        MailMessage objEmail = new MailMessage();

        //Remetente do e-mail.
        objEmail.From = new MailAddress(sRemetente);

        //Destinatários do e-mail.
        string[] sDestinatarios = sListaDestinatario.Split(';');
        foreach (string sDestinatario in sDestinatarios)
        {
            if (sDestinatario != "")
            {
                objEmail.To.Add(sDestinatario);
            }
        }

        //Enviar cópia para.
        string[] sCopias = sListaCopia.Split(';');
        foreach (string sCopia in sCopias)
        {
            if (sCopia != "")
            {
                objEmail.To.Add(sCopia);
            }
        }

        //Enviar cópia oculta para.
        string[] sCopiasOculta = sListaCopiaOculta.Split(';');
        foreach (string sCopiaOculta in sCopiasOculta)
        {
            if (sCopiaOculta != "")
            {
                objEmail.Bcc.Add(sCopiaOculta);
            }
        }

        //Define a prioridade do e-mail.
        objEmail.Priority = MailPriority.High;

        //Formato do e-mail HTML
        objEmail.IsBodyHtml = bFormatoHTML;

        //Título do e-mail.
        objEmail.Subject = sTitulo;

        //Assunto do e-mail.
        objEmail.Body = sAssunto;

        //Anexa arquivos.
        string[] sCaminhosAnexos = sListaCaminhoAnexo.Split(';');
        foreach (string sCaminhoAnexo in sCaminhosAnexos)
        {
            if (sCaminhoAnexo != "")
            {
                Attachment aArquivo = new Attachment(sCaminhoAnexo);
                objEmail.Attachments.Add(aArquivo);
            }
        }

        //Para caracteres especiais charset para "ISO-8859-1"
        objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

        //Cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();

        //Servidor de E-mail
        objSmtp.Host = sServer;
        //Envio pela Rede em vez de pasta em disco
        objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //Utilizar SSL
        objSmtp.EnableSsl = bSSL;
        //Porta de comunicação
        objSmtp.Port = iPortaSmtp;
        //Timeout em milisegundos (1000 = 1 seg)
        objSmtp.Timeout = 10000;
        //Não Utilizar crediciais default
        objSmtp.UseDefaultCredentials = false;

        if (sUsuario != "" && sSenha != "")
        {
            objSmtp.Credentials = new NetworkCredential(sUsuario, sSenha);
        }

        objSmtp.Send(objEmail);
        objEmail.Dispose();
    }

    public void EnviarEmailAgenda(string sRemetente, string sListaDestinatario, string sListaCopia, string sListaCopiaOculta, bool bFormatoHTML, string sTitulo, string sAssunto, string sListaCaminhoAnexo, Attachment attach, string sServer, bool bSSL, int iPortaSmtp, string sUsuario, string sSenha)
    {
        MailMessage objEmail = new MailMessage();

        //Remetente do e-mail.
        objEmail.From = new MailAddress(sRemetente);

        //Destinatários do e-mail.
        string[] sDestinatarios = sListaDestinatario.Split(';');
        foreach (string sDestinatario in sDestinatarios)
        {
            if (sDestinatario != "")
            {
                objEmail.To.Add(sDestinatario);
            }
        }

        //Enviar cópia para.
        string[] sCopias = sListaCopia.Split(';');
        foreach (string sCopia in sCopias)
        {
            if (sCopia != "")
            {
                objEmail.To.Add(sCopia);
            }
        }

        //Enviar cópia oculta para.
        string[] sCopiasOculta = sListaCopiaOculta.Split(';');
        foreach (string sCopiaOculta in sCopiasOculta)
        {
            if (sCopiaOculta != "")
            {
                objEmail.Bcc.Add(sCopiaOculta);
            }
        }

        //Define a prioridade do e-mail.
        objEmail.Priority = MailPriority.High;

        //Formato do e-mail HTML
        objEmail.IsBodyHtml = bFormatoHTML;

        //Título do e-mail.
        objEmail.Subject = sTitulo;

        //Assunto do e-mail.
        objEmail.Body = sAssunto;

        //Anexa arquivos.
        string[] sCaminhosAnexos = sListaCaminhoAnexo.Split(';');
        foreach (string sCaminhoAnexo in sCaminhosAnexos)
        {
            if (sCaminhoAnexo != "")
            {
                Attachment aArquivo = new Attachment(sCaminhoAnexo);
                objEmail.Attachments.Add(aArquivo);
            }
        }


        if (attach != null)
        {
            objEmail.Attachments.Add(attach);
        }

        //Para caracteres especiais charset para "ISO-8859-1"
        objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

        //Cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();

        //Servidor de E-mail
        objSmtp.Host = sServer;
        //Envio pela Rede em vez de pasta em disco
        objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //Utilizar SSL
        objSmtp.EnableSsl = bSSL;
        //Porta de comunicação
        objSmtp.Port = iPortaSmtp;
        //Timeout em milisegundos (1000 = 1 seg)
        objSmtp.Timeout = 10000;
        //Não Utilizar crediciais default
        objSmtp.UseDefaultCredentials = false;

        if (sUsuario != "" && sSenha != "")
        {
            objSmtp.Credentials = new NetworkCredential(sUsuario, sSenha);
        }

        objSmtp.Send(objEmail);
        objEmail.Dispose();
    }
}
