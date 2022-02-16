using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildrenToUsersDataClass
{
    public string firstChildID, secondChildID, thirdChildID;
    public AddChildrenToUsersDataClass(string firstChildID)
    {
        this.firstChildID = firstChildID;
    }
    /// <summary>
    /// Each parent has a maximum of 3 children as per the policy of AKG kids, as such a record of three children is generated with blank value
    /// </summary>
    /// <param name="firstChildID">ID of Child one</param>
    /// <param name="secondChildID"> ID of Child two</param>
    /// <param name="thirdChildID">ID of child three</param>
    public AddChildrenToUsersDataClass(string firstChildID, string secondChildID, string thirdChildID)
    {
        this.firstChildID = firstChildID;
        this.secondChildID = secondChildID;
        this.thirdChildID = thirdChildID;
    }
}
