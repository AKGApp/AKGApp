public class ChildrenInfrormation
{
    public string parentID, childID, childDesignation, childDateOfBirth, childName, registrationDate;
    public bool isActive;
    
    public ChildrenInfrormation()
    {

    }

    /// <summary>
    /// Children information class to add informaiton to the firebase database
    /// </summary>
    /// <param name="parentID">the parent firebase user ID.</param>
    /// <param name="childID">A unique KEY genereated by the Firebase Push().key method.</param>
    /// <param name="childDesignation">Is the child a son or daughter.</param>
    /// <param name="childDateOfBirth">The Child date of birth.</param>
    /// <param name="childName">The childs name.</param>
    /// <param name="registrationDate">Get the server date of the registration,</param>
    /// <param name="isActive">Should the account be active? true by defualt.</param>
    public ChildrenInfrormation(string parentID, string childID, string childDesignation, string childDateOfBirth, string childName, string registrationDate, bool isActive)
    {
        this.parentID = parentID;
        this.childID = childID;
        this.childDesignation = childDesignation;
        this.childDateOfBirth = childDateOfBirth;
        this.childName = childName;
        this.registrationDate = registrationDate;
        this.isActive = isActive;
    }
}
