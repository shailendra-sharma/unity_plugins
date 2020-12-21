package com.example.android_plugin;

import android.content.Context;
import android.provider.Settings;
import android.util.Log;

public class AndroidUtil {
    private static String deviceId;

    public static String getAndroidId(Context context) {
        if (deviceId == null || deviceId.isEmpty()) {
            try {
                deviceId = Settings.System.getString(context.getContentResolver(), Settings.Secure.ANDROID_ID);
            } catch (Exception e) {
                Log.d("AndroidUtil", "Could not fetch android device id");
                e.printStackTrace();
            }
        }

        return deviceId;
    }

    public static String getPrivateAppDataFolder(Context context) {
        if (context.getApplicationInfo() != null) {
            return context.getApplicationInfo().dataDir;
        }
        return null;
    }
}
