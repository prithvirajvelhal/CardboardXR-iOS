# LION CINEMA VR — development build

This update adds a procedural seated IMAX-style auditorium as a new CardboardXR demo. It is generated automatically during the existing `ProjectBootstrap.StereoBuilder.BuildIos` cloud build and registered in the app library as **LION CINEMA VR**. It does not replace the existing 360 Video demo or change the existing iOS signing workflow.

## Included now
- Dark auditorium, carpeted floor, stage, curved giant screen, red seats, gold trim and aisle lights.
- Uses the existing CardboardXR Core scene and its stereo/head-tracking pipeline.
- Separate demo scene; existing apps remain available.

## Not yet included
YouTube playback is **not connected in this build**. Unity's built-in `VideoPlayer` cannot play a normal `youtube.com/watch` page URL. A compliant implementation needs an iOS WebKit/YouTube IFrame integration and a way to render the WebKit video into a Unity texture for both stereo eyes. A normal WKWebView overlay would appear as a flat phone overlay, not correctly on the curved stereo screen. This package deliberately does not pretend that YouTube streaming already works.

## Build
Use the same GitHub Actions workflow and Unity version already used for the successful CardboardXR iOS build (Unity 6000.6.0f1). Upload this project update to a separate branch/repository first. Existing iOS signing and IPA packaging steps can remain unchanged.

## First test
Install the IPA, open the app library and select **LION CINEMA VR**. Check that the auditorium appears and that head rotation works. The screen is currently a visual placeholder for the future video texture.
