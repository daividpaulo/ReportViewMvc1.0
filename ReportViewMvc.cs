using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Reporting.WebForms;


namespace Relatorios.Util
{
    public class ReportViewMvc
    {
        private  LocalReport Relatorio { get; set; }
        private  readonly string nomeRelatorio;
        private  readonly string localRelatorio;
        private  readonly List<ReportDataSource> sourcesRelatorio;
        private  readonly List<ReportDataSource> sourcesSubRelatorios;
         


        public ReportViewMvc(string nomeRelatorio, string localRelatorio)
        {
            this.nomeRelatorio = nomeRelatorio;
            this.localRelatorio = localRelatorio;
            this.sourcesRelatorio = new List<ReportDataSource>();
            this.sourcesSubRelatorios = new List<ReportDataSource>();
            Relatorio = new LocalReport { ReportPath = localRelatorio + "\\" + nomeRelatorio };
        }

        public Tuple<byte[],string> GerarRelatorio(TipoArquivo gerarComo)
        {

            foreach (var source in sourcesRelatorio)
            {
                Relatorio.DataSources.Add(source);
            }

            if (sourcesSubRelatorios != null && sourcesSubRelatorios.Any())
            {
                Relatorio.SubreportProcessing += RenderizaSubRelatorios;
            }

            string reportType = gerarComo.ToString();
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            byte[] bytes;

            string deviceInfo = "<DeviceInfo>" +
                                " <OutputFormat>" + gerarComo.ToString() + "</OutputFormat>" +
                                // " <PageWidth>9in</PageWidth>" +
                                // " <PageHeight>11in</PageHeight>" +
                                // " <MarginTop>0,7874in</MarginTop>" +
                                // " <MarginLeft>2in</MarginLeft>" +
                                // " <MarginRight>2in</MarginRight>" +
                                // " <MarginBottom>0,7874in</MarginBottom>" +
                                "</DeviceInfo>";
            
            bytes = Relatorio.Render(reportType,deviceInfo,out mimeType,out encoding,out fileNameExtension,out streams,out warnings);
            
            var relatorioGerado = new Tuple<byte[], string>(bytes, mimeType);

            return relatorioGerado;

        }

        

        private void RenderizaSubRelatorios(object sender, SubreportProcessingEventArgs e)
        {

            // int codPedido = Convert.ToInt32(e.Parameters["codPedido"].Values[0]); 
            //-- Caso precise de obter parametro do relatorio e setar no where na lista antes de Add. Ex: .Add(Source.Where(x => x.blabla = codPedido))

            foreach (var source in sourcesSubRelatorios)
            {
                e.DataSources.Add(source);
            }


        }

        public void AddDataSourceNoRelatorio(string nomeDataSet,ICollection<Object> lista)
        {
            sourcesRelatorio.Add(new ReportDataSource(nomeDataSet,lista.First()));
        }

        public void AddDataSourceNoSubRelatorio(string nomeDataSet, ICollection<Object> lista)
        {
            sourcesSubRelatorios.Add(new ReportDataSource(nomeDataSet, lista.First()));
        }


        public enum TipoArquivo
        {
            PDF,
            Excel,
            Word
        }

    }
}
