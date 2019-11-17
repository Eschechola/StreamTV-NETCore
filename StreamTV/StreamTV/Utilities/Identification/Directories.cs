namespace StreamTV.Utilities.Identification
{
    public class Directories
    {
        public string GetFileNameByLink(string link)
        {
            //ele irá pegar todos os caracteres após
            string palavraAchar = "Videos/";
            //pega todos os caracteres após a evidencia da palavra, adicionado o .lenght senao ele inclui a palavra videos
            var nomeArquivo = link.Substring(link.LastIndexOf(palavraAchar) + palavraAchar.Length);
            return nomeArquivo;
        }
    }
}
