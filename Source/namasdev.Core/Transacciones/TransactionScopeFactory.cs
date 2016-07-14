using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace namasdev.Transacciones
{
    public class TransactionScopeFactory
    {
        public static TransactionScope Crear(
            TransactionScopeOption transaccionAUsar = TransactionScopeOption.Required, 
            IsolationLevel nivelAislamiento = IsolationLevel.ReadCommitted)
        {
            return new TransactionScope(transaccionAUsar, new TransactionOptions { IsolationLevel = nivelAislamiento });
        }
    }
}
