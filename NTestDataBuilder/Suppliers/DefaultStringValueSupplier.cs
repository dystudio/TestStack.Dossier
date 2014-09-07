﻿using Ploeh.AutoFixture;

namespace NTestDataBuilder.Suppliers
{
    /// <summary>
    /// Supplies default anonymous value for a string type.
    /// </summary>
    public class DefaultStringValueSupplier : IAnonymousValueSupplier
    {
        /// <inheritdoc />
        public bool CanSupplyValue<TObject, TValue>(string propertyName)
        {
            return typeof (TValue) == typeof(string);
        }

        /// <inheritdoc />
        public TValue GenerateAnonymousValue<TObject, TValue>(AnonymousValueFixture fixture, string propertyName)
        {
            return (TValue) (object) fixture.Fixture.Create(propertyName);
        }
    }
}
