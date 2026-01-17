using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.EnumClasses
{
    public enum LoginLogStatus
    {
        Login = 1,
        Logout = 2,
        PasswordChange = 3,
        DefaultPasswordChange = 4,
        ForceFullLogout = 5,
        InvalidPassword = 6,
        PasswordReset = 7,
        MobileUpdated = 8
    }

    public enum EnumRoles
    {
        ADMIN = 1,
        DISTRICT = 2,
        DISTRICT_VENDOR = 3,
        TEHSIL_SDM = 8,
        ADM = 9,
        OCK_ADM=28,
        ESDOffice=35
    }
    public enum EnumUserType
    {
        Admin = 1,
        District = 2,
        Tehshil = 3
    }
    public enum LoginUserType
    {
        OCK_ADM=1,
        ESD_OFFICE=2
    }
    public enum OnlineVotersStatus
    {
        Approved = 2,
        Reject = 3,
        Save = 1,
        Default = 0
    }
    public enum ApplicationStatus
    {
        Success = 1,
        Catch_Error = 2,
        SuplimentryLevel_Id_Null = 3,
        IsEntryFreezed = 4,
        IsEntryNotFreezed = 5,
    }
    public enum IsEntryFreezed 
    {
        IsEntryFreezed_true  = 1,
        IsEntryFreezed_false = 0,
    }
    public enum VoterApproveStatus
    {
        Pending = 0,
        Approve = 1,
        Reject = 2,
    }
    public enum AreaType
    {
        Nagariya=1,
        Panchayat=2
    }
}


