﻿using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain.Core.Model
{
    public class Money : ValueObject<Money>
    {
        public readonly decimal Value;
        public Guid ProdutoId { get; set; }

        public Money() : this(0m)
        {
        }

        public Money(Guid produtoId, decimal value)
        {
            Value = value;
            ProdutoId = produtoId;
        }

        public Money(decimal value)
        {
            Value = value;
        }

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        public Money Subtract(Money money)
        {
            return new Money(Value - money.Value);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Value };
        }
    }
}
