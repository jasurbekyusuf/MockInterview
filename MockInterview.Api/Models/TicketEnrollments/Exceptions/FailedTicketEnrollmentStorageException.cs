// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    internal class FailedTicketEnrollmentStorageException
    {
        private SqlException sqlException;

        public FailedTicketEnrollmentStorageException(SqlException sqlException)
        {
            this.sqlException = sqlException;
        }
    }
}