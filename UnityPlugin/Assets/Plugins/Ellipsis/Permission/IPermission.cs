using System.Collections.Generic;

namespace Ellipsis.Permission
{
    public enum PermissionState
    {
        Denied = 0,
        Granted = 1,
        ShouldAsk = 2,
    }

    public enum AndroidPermiossionType
    {
        READ_CALENDAR,
        WRITE_CALENDAR,
        CAMERA,
        READ_CONTACTS,
        WRITE_CONTACTS,
        GET_ACCOUNTS,
        ACCESS_FINE_LOCATION,
        ACCESS_COARSE_LOCATION,
        RECORD_AUDIO,
        READ_PHONE_STATE,
        READ_PHONE_NUMBERS,
        CALL_PHONE,
        ANSWER_PHONE_CALLS,
        READ_CALL_LOG,
        WRITE_CALL_LOG,
        ADD_VOICEMAIL,
        USE_SIP,
        PROCESS_OUTGOING_CALLS,
        BODY_SENSORS,
        SEND_SMS,
        RECEIVE_SMS,
        READ_SMS,
        RECEIVE_WAP_PUSH,
        RECEIVE_MMS,
        READ_EXTERNAL_STORAGE,
        WRITE_EXTERNAL_STORAGE,
    }

    public enum IOSPermissionType
    {
        NSPhotoLibraryAddUsageDescription,
        NSPhotoLibraryUsageDescription,
        NSCameraUsageDescription,
        NSLocationAlwaysUsageDescription,
        NSLocationWhenInUseUsageDescription,
        NSLocationUsageDescription,
        NSContactsUsageDescription,
        NSCalendarsUsageDescription,
        NSRemindersUsageDescription,
        NSHealthShareUsageDescription,
        NSHealthUpdateUsageDescription,
        NFCReaderUsageDescription,
        NSBluetoothPeripheralUsageDescription,
        NSMicrophoneUsageDescription,
        NSSiriUsageDescription,
        NSSpeechRecognitionUsageDescription,
        NSMotionUsageDescription,
        NSVideoSubscriberAccountUsageDescription,
        NSAppleMusicUsageDescription,
        NSFaceIDUsageDescription,
    }

    public delegate void AndroidPermissionResult(AndroidPermiossionType permission, PermissionState result);
    public delegate void AndroidPermissionResultMultiple(Dictionary<AndroidPermiossionType, PermissionState> result);

    public delegate void IOSPermissionResult(IOSPermissionType permission, PermissionState result);
    public delegate void IOSPermissionResultMultiple(Dictionary<IOSPermissionType, PermissionState> result);

}
