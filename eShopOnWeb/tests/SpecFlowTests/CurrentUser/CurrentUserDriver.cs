namespace SpecFlowTests.CurrentUser
{
    public class CurrentUserDriver
    {
        private readonly CurrentUserContext _currentUserContext;

        public CurrentUserDriver(CurrentUserContext currentUserContext)
        {
            _currentUserContext = currentUserContext;
        }

        public void GivenIAmLoggedInAs(string username)
        {
            _currentUserContext.CurrentUserName = username;
        }
    }
}