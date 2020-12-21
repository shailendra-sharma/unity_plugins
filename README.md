### This Contains two plugins
# 1. UUID Plugin
**Android**: Used [Settings.Secure.ANDROID_ID](https://developer.android.com/reference/android/provider/Settings.Secure#ANDROID_ID)  
**iOS** : Used [CFUUID](https://developer.apple.com/documentation/corefoundation/cfuuid-rci)  
- Both UUID in Android and iOS will only be reset in below cases  
-- Device Reset  
-- If User Change Google or Apple ID in device  
-- iOS UUID will be unique for each signin certificate e.g Development/Enterprise/Distribution will each have unique UUID fro the same application

# 2. SaveData Plugin
Use AES encryption and zLib compression to store data  

> **Note**: Android Demo apk is included in this repo while iOS app is not included because Signin Certificate is not available for distribution
We need to add Security.framework to create iOS Demo App from the Unty Project
