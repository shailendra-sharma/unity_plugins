#import "FDKeychain.h"
#import "FDNullOrEmpty.h"

@interface DeviceInfo : NSObject
+(NSString*) getUUID:(NSString*) keychainService :(NSString*) KeychainUUID;
+(NSString *)generateUUIDWithKeychainService:(NSString *)keyChainService withUUIDKey:(NSString *)keychainUUID;
@end

@implementation DeviceInfo

+(NSString*) getUUID:(NSString*) Keychain_Service :(NSString*) Keychain_UUID
{
    if(Keychain_Service != nil && Keychain_UUID != nil) {
        NSString *deviceUUIDString = [DeviceInfo generateUUIDWithKeychainService:Keychain_Service withUUIDKey:Keychain_UUID];
        NSLog(@"IOSPlugin: DeviceUUID = %@", deviceUUIDString);
        return deviceUUIDString;
    }
    else {
        return @"";
    }
}

#pragma Create UUID
+(NSString *)generateUUIDWithKeychainService:(NSString *) keyChainService withUUIDKey:(NSString *)keychainUUID {
    NSString *CFUUID = nil;
    
    if (![FDKeychain itemForKey: keychainUUID
                     forService: keyChainService
                          error: nil]) {
        
        CFUUIDRef uuid = CFUUIDCreate(kCFAllocatorDefault);
        
        CFUUID = (NSString *)CFBridgingRelease(CFUUIDCreateString(kCFAllocatorDefault, uuid));
        
        [FDKeychain saveItem: CFUUID
                      forKey: keychainUUID
                  forService: keyChainService
                       error: nil];
        
    } else {

        CFUUID = [FDKeychain itemForKey: keychainUUID
                             forService: keyChainService
                                  error: nil];
    }
    
    return CFUUID;
}
@end

extern "C"
{
    const char* _GetUUIDWithPassword (const char *Keychain_Service,const char *Keychain_UUID)
    {
        NSString* keychainService = [NSString stringWithUTF8String:Keychain_Service];
        NSString* keychainUUID= [NSString stringWithUTF8String:Keychain_UUID];
     
        NSString* deviceUUID = [DeviceInfo getUUID:keychainService :keychainUUID];
        
        return strdup([deviceUUID UTF8String]);
    }

    const char* _GetUUID ()
    {
        return _GetUUIDWithPassword("GluTest", "GLU@1234");
    }
}
