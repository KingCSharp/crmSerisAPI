using System;
using System.Threading.Tasks;
using crmSeries.Core.Data;

namespace crmSeries.Core.Mediator.Decorators
{
    public class AdminContextAttribute : Attribute
    {
    }

    public class HeavyEquipmentContextAttribute : Attribute
    {
    }

    // DEV NOTE: DO NOT alter the below classes unless you really understand the architecture. 
    public class AdminTransactionBaseHandler<TRequest, TResult> where TResult : Response, new()
    {
        private readonly AdminContext _context;

        public AdminTransactionBaseHandler(AdminContext context)
        {
            _context = context;
        }

        public async Task<TResult> HandleAsync(TRequest request, Func<Task<TResult>> processRequest)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                return await processRequest();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var result = await processRequest();
                if (result.HasErrors)
                {
                    transaction.Rollback();
                    return result;
                }

                transaction.Commit();
                return result;
            }
        }
    }

    public class AdminTransactionHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        private readonly Func<IRequestHandler<TRequest>> _decorateeFactory;
        private readonly AdminTransactionBaseHandler<TRequest, Response> _transactionHandler;

        public AdminTransactionHandler(Func<IRequestHandler<TRequest>> decorateeFactory,
            AdminTransactionBaseHandler<TRequest, Response> transactionHandler)
        {
            _decorateeFactory = decorateeFactory;
            _transactionHandler = transactionHandler;
        }

        public Task<Response> HandleAsync(TRequest request)
        {
            return _transactionHandler.HandleAsync(request, () => _decorateeFactory().HandleAsync(request));
        }
    }

    public class AdminTransactionHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        private readonly Func<IRequestHandler<TRequest, TResult>> _decorateeFactory;
        private readonly AdminTransactionBaseHandler<TRequest, Response<TResult>> _transactionHandler;

        public AdminTransactionHandler(Func<IRequestHandler<TRequest, TResult>> decorateeFactory,
            AdminTransactionBaseHandler<TRequest, Response<TResult>> transactionHandler)
        {
            _decorateeFactory = decorateeFactory;
            _transactionHandler = transactionHandler;
        }

        public Task<Response<TResult>> HandleAsync(TRequest request)
        {
            return _transactionHandler.HandleAsync(request, () => _decorateeFactory().HandleAsync(request));
        }
    }

    public class HeavyEquipmentTransactionBaseHandler<TRequest, TResult> where TResult : Response, new()
    {
        private readonly HeavyEquipmentContext _context;

        public HeavyEquipmentTransactionBaseHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<TResult> HandleAsync(TRequest request, Func<Task<TResult>> processRequest)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                return await processRequest();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var result = await processRequest();
                if (result.HasErrors)
                {
                    transaction.Rollback();
                    return result;
                }

                transaction.Commit();
                return result;
            }
        }
    }

    public class HeavyEquipmentTransactionHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        private readonly Func<IRequestHandler<TRequest>> _decorateeFactory;
        private readonly HeavyEquipmentTransactionBaseHandler<TRequest, Response> _transactionHandler;

        public HeavyEquipmentTransactionHandler(Func<IRequestHandler<TRequest>> decorateeFactory,
            HeavyEquipmentTransactionBaseHandler<TRequest, Response> transactionHandler)
        {
            _decorateeFactory = decorateeFactory;
            _transactionHandler = transactionHandler;
        }

        public Task<Response> HandleAsync(TRequest request)
        {
            return _transactionHandler.HandleAsync(request, () => _decorateeFactory().HandleAsync(request));
        }
    }

    public class HeavyEquipmentTransactionHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        private readonly Func<IRequestHandler<TRequest, TResult>> _decorateeFactory;
        private readonly HeavyEquipmentTransactionBaseHandler<TRequest, Response<TResult>> _transactionHandler;

        public HeavyEquipmentTransactionHandler(Func<IRequestHandler<TRequest, TResult>> decorateeFactory,
            HeavyEquipmentTransactionBaseHandler<TRequest, Response<TResult>> transactionHandler)
        {
            _decorateeFactory = decorateeFactory;
            _transactionHandler = transactionHandler;
        }

        public Task<Response<TResult>> HandleAsync(TRequest request)
        {
            return _transactionHandler.HandleAsync(request, () => _decorateeFactory().HandleAsync(request));
        }
    }
}