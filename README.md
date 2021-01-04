ReportViewMvc1.0
================

DLL Relatório MVC With ReportView (Relatório Gerado em PDF,XLS,DOC,não precisa criar arquivos .aspx, ou seja, totalmente MVC)

1º) Criar uma DLL no projeto MVC, para que os relatorios sejam criados dentro dela.

2º) Criar uma Folder(Pasta) com nome Util dentro da DLL criada, e salvar dentro dela as classes
ReportViewMvc.cs e ReportViewMvcUtil.

3º) Adicionar um ou mais DataSource(s) ao projeto DLL de relatorios criado.
    Para isso basta clicar na DLL criada, depois no menu Data > Add New Data Source
    obs: Se o projeto Web principal estiver selecionado esta opção no menu Data não estara visivel.
    
3.1 - Escolha o tipo object

3.2 - Escolha as classes de modelo que deseja para o(s) relatorio(s), talvez seja necessario clicar no botão adicionar referencia e adicionar sua DLL de Modelos de Dominio

3.3 - Clicar em finish 

4º) Agora podemos criar relatorios .rdlc, adicione um objeto table por exemplo ao relatorio criado, escolha o data source para esse objeto e mude o nome do dataSet antes de finalizar, pois este nome será necessario na hora de popular os dados através do controller.

5º) Chamar e Gerar o relatorio a partir do Controller:

     public ActionResult GerarRelatorioMvc(){
        
            var utilRelMvc = new RelatorioMvcUtil();

            const string nomeDlldeRelatorio = "Relatorios";
            const string nomePastaLocalDoRelatorio = "\\DeCliente";
            const string nomeRelatorio = "RelClientes.rdlc";

            var localRelatorios = utilRelMvc.RetornaPathDllDeRelatorio(nomeDllRelatorio: nomeDlldeRelatorio) + nomePastaLocalDoRelatorio;
           
            var relatorio = new RelatorioMvc(nomeRelatorio, localRelatorios);

            var itens = new List<Clientes>();
            itens.Add(serviceCliente.GetTodosClientes().Where(x => x.Email != null));

            relatorio.AddDataSourceNoRelatorio(nomeDataSet: "DSCliente", lista: new[] { itens });
            
            
            Tuple<byte[], string> relatorioPdf = relatorio.GerarRelatorio(RelatorioMvc.TipoArquivo.PDF);

            return File(fileContents: relatorioPdf.Item1, contentType: relatorioPdf.Item2);

        }
