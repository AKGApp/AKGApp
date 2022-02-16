public class UserParentInformation
{
    public string userID, userName, signupDate, signupLocation, dateOfBirth, parentDesignation, parentEmail, parentNumber;
    public UserParentInformation()
    {}
    public UserParentInformation(string userID, string userName, string signupDate, string signupLocation, string dateOfBirth, string parentDesignation, string parentEmail, string parentNumber)
    {
        this.userID = userID;
        this.userName = userName;
        this.signupDate = signupDate;
        this.signupLocation = signupLocation;
        this.dateOfBirth = dateOfBirth;
        this.parentDesignation = parentDesignation;
        this.parentEmail = parentEmail;
        this.parentNumber = parentNumber;
    }
}