using System;
using System.Linq;
using EdFi.Ods.Common.Database;
using EdFi.Ods.Common.Exceptions;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;

namespace EdFi.Ods.Extensions.Publishing.Feature.DatabaseNaming
{
    /// <summary>
    /// Implements a decorator that appends a snapshot-specific suffix to the database name
    /// to allow API requests to be serviced off a static snapshot of the ODS for the purposes
    /// of maintaining a fully isolated context for client API publishing using Change Queries.
    /// </summary>
    public class SnapshotSuffixDatabaseNameProviderDecorator : IDatabaseNameProvider
    {
        private readonly IDatabaseNameProvider _next;
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SnapshotSuffixDatabaseNameProviderDecorator(
            IDatabaseNameProvider next,
            ISnapshotContextProvider snapshotContextProvider)
        {
            _next = next;
            _snapshotContextProvider = snapshotContextProvider;
        }

        /// <summary>
        /// Gets the database name with a snapshot-specific suffix appended.
        /// </summary>
        /// <returns>The snapshot database name.</returns>
        /// <exception cref="FormatException">Occurs if the snapshot identifier in context
        /// is not a 32-character alphanumeric value.</exception>
        public string GetDatabaseName()
        {
            var snapshotContext = _snapshotContextProvider.GetSnapshotContext();
            
            if (snapshotContext == null || string.IsNullOrEmpty(snapshotContext.SnapshotIdentifier))
                return _next.GetDatabaseName();

            // To prevent possible tampering, snapshot identifier must be a 32 character long value (accommodates a GUID)
            if (snapshotContext.SnapshotIdentifier.Length != 32)
                throw new FormatException("Invalid value for snapshot identifier.");
            
            // To prevent possible tampering, snapshot identifier must only contain letters or numbers.
            if (snapshotContext.SnapshotIdentifier.Any(c => !(char.IsLetter(c) || char.IsDigit(c))))
                throw new FormatException("Invalid value for snapshot identifier.");

            return _next.GetDatabaseName() + $"_SS{snapshotContext.SnapshotIdentifier}";
        }
    }
}
