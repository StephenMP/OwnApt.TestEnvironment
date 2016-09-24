using System.Threading.Tasks;
using Xunit;

namespace TestEnvironment.Tests.Component.Environment.Configuration
{
    public class SqlConfigurationFeatures
    {
        #region Private Fields

        private readonly SqlConfigurationSteps steps;

        #endregion Private Fields

        #region Public Constructors

        public SqlConfigurationFeatures()
        {
            this.steps = new SqlConfigurationSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void CanAddDbContext()
        {
            this.steps.GivenIHaveASqlConfiguration();
            this.steps.WhenIAddADbContext();
            this.steps.ThenICanVerifyICanAddDbContext();
        }

        [Fact]
        public async Task CanImportMultipleSqlDataAsync()
        {
            this.steps.GivenIHaveASqlConfiguration();
            this.steps.GivenIHaveADbContext();
            this.steps.GivenIHaveMultipleDataToImport();
            await this.steps.WhenIImportSqlDataAsync();
            this.steps.ThenICanVerifyICanImportMultipleSqlDataAsync();
        }

        [Fact]
        public async Task CanImportSqlDataAsync()
        {
            this.steps.GivenIHaveASqlConfiguration();
            this.steps.GivenIHaveADbContext();
            this.steps.GivenIHaveDataToImport();
            await this.steps.WhenIImportSqlDataAsync();
            this.steps.ThenICanVerifyICanImportSqlDataAsync();
        }

        [Fact]
        public void CanNewUpSqlConfiguration()
        {
            this.steps.GivenIHaveASqlConfiguration();
            this.steps.ThenICanVerifyICanNewUpSqlConfiguration();
        }

        [Fact]
        public void CanRetrieveDbContextOptions()
        {
            this.steps.GivenIHaveASqlConfiguration();
            this.steps.GivenIAddedADbContext();
            this.steps.WhenIRetrieveADbContext();
            this.steps.ThenICanVerifyICCanRetrieveDbContextOptions();
        }

        #endregion Public Methods
    }
}
