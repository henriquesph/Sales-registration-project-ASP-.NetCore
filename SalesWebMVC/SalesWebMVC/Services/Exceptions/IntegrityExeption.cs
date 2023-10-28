// Integridade referencial: Integridade referencial é um conceito relacionado à chaves estrangeiras. Este conceito diz que o valor que é chave estrangeira em uma tabela destino, deve ser chave primária de algum registro na tabela origem. Quando essa regra é desrespeitada, então temos o caso em que a integridade referencial é violada. (DbUpdateExceptions)

// O erro que corrigido aqui é que quando se apagava um Seller associado a vendas  caia nessa excessão

//O que é uma constraint no SQL?
//Constraints (restrições) mantém os dados do usuário restritos, e assim evitam que dados inválidos sejam inseridos no banco. A mera definição do tipo de dado para uma coluna é por si só um constraint. Por exemplo, uma coluna de tipo DATE restringe o conteúdo da mesma para datas válidas.

namespace SalesWebMVC.Services.Exceptions
{
    public class IntegrityExeption : ApplicationException
    {
        public IntegrityExeption(string message) : base(message) 
        {
        }
    }
}
