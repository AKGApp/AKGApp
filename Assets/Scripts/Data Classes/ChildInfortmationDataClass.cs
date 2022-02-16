using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildInfortmationDataClass
{
    public string childName, childID, childParentID, childDOB, childEnnroledDate, childGender, childImage;

    public ChildInfortmationDataClass() { }

    public ChildInfortmationDataClass(string childName, string childID, string childParentID, string childDOB, string childEnnroledDate, string childGender)
    {
        this.childName = childName;
        this.childID = childID;
        this.childParentID = childParentID;
        this.childDOB = childDOB;
        this.childEnnroledDate = childEnnroledDate;
        this.childGender = childGender;
    }
}
