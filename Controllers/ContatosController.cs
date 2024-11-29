using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace Desapego.Controllers
{
    public class ContatosController : Controller
    {
        // Ação para a página inicial (Index)
        public IActionResult Index()
        {
            return View();
        }

        // Ação para a página de contatos
        public IActionResult PageContatos()
        {
            return View();
        }

        // Ação para inserir dados (funcionário e produto)
        [HttpGet]
        public IActionResult Inserir(string nome, string sobreNome, int cpf, string email, string modelo, string ano, string preco)
        {
            string msg = "";
            try
            {
                // Inserção dos dados do funcionário (sem alterações)
                string sqlFuncionario = $"INSERT INTO funcionario(nome, sobreNome, cpf, email) VALUES('{nome}', '{sobreNome}', {cpf}, '{email}')";

                // Inserção dos dados do produto (sem o campo numeroProduto)
                string sqlProduto = $"INSERT INTO produto(modelo, ano, preco) VALUES('{modelo}', '{ano}', '{preco}')";

                // Estabelece a conexão com o banco de dados
                SQLiteConnection con = pegarConexao();
                con.Open();

                // Executa o comando de inserção para o funcionário
                SQLiteCommand cmdFuncionario = new SQLiteCommand(sqlFuncionario, con);
                cmdFuncionario.ExecuteNonQuery();

                // Executa o comando de inserção para o produto (sem o numeroProduto)
                SQLiteCommand cmdProduto = new SQLiteCommand(sqlProduto, con);
                cmdProduto.ExecuteNonQuery();

                con.Close();

                msg = "Dados inseridos com sucesso!";
            }
            catch (Exception e)
            {
                msg = "Não foi possível inserir! " + e.Message;
            }

            return Json(msg);
        }

        // Função para pegar a conexão com o banco de dados e garantir que as tabelas existam
        public SQLiteConnection pegarConexao()
        {
            SQLiteConnection sqlite_conn;
            string stringConnection = "Data Source=Academia.db; Version = 3; New = True; Compress = True; ";
            sqlite_conn = new SQLiteConnection(stringConnection);

            sqlite_conn.Open();

            // Criação da tabela funcionario se não existir
            string sqlFuncionario = $"CREATE TABLE IF NOT EXISTS 'funcionario' ('cpf' INTEGER, 'nome' TEXT, 'sobreNome' TEXT, 'email' TEXT, 'senha' TEXT, PRIMARY KEY('cpf' AUTOINCREMENT))";
            SQLiteCommand comandoFuncionario = new SQLiteCommand(sqlFuncionario, sqlite_conn);
            comandoFuncionario.ExecuteNonQuery();

            // Criação da tabela produto se não existir
            string sqlProduto = $"CREATE TABLE IF NOT EXISTS 'produto' ('numeroProduto' INTEGER PRIMARY KEY AUTOINCREMENT, 'modelo' TEXT, 'ano' TEXT, 'preco' TEXT)";
            SQLiteCommand comandoProduto = new SQLiteCommand(sqlProduto, sqlite_conn);
            comandoProduto.ExecuteNonQuery();

            sqlite_conn.Close();

            return new SQLiteConnection(stringConnection);
        }
    }
}
