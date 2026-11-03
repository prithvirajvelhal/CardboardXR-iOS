# CardboardXR — shortest Windows-to-iPhone build route

This is the original Unity project plus a GitHub Actions workflow that asks a hosted macOS runner to run Unity's iOS export and Xcode. No physical Mac is needed.

## What is already prepared

- Unity project remains pinned to `6000.6.0f1`.
- Workflow: `.github/workflows/ios-cloud-build.yml`.
- Unity's iOS build method: `ProjectBootstrap.StereoBuilder.BuildIos`.
- Xcode project output path verified from the source: `Builds/iOS`.
- Workflow compiles an **unsigned** IPA and uploads it with the Xcode project for diagnosis.

## Only actions you must do

1. Create a **public** repository on GitHub (or fork `brenpoly/CardboardXR`).
2. Upload the *contents* of this folder to the repository root. Confirm `.github/workflows/ios-cloud-build.yml` is present.
3. Obtain a Unity Personal licence activation file for Unity `6000.6.0f1` and add its contents as repository secret `UNITY_LICENSE` under Settings → Secrets and variables → Actions. Do not commit or share the licence file. If Unity's activation method changes or Unity Personal cannot be activated headlessly, the first cloud run will expose that error.
4. Open Actions → **CardboardXR iOS cloud build** → Run workflow.
5. Download artifact `CardboardXR-iOS-unsigned` from the completed run.

## Critical iPhone installation limitation

The workflow deliberately produces an **unsigned** IPA. An unsigned IPA cannot be installed directly. It must be signed with a valid Apple Development certificate and provisioning profile that includes the iPhone's UDID. A free Apple Personal Team can normally create a short-lived development-signed app, but certificate/profile creation and device registration are separate steps. AltStore/AltServer is a signing/installation helper, not a way around Apple's signing requirement. The cloud build cannot register or pair your physical iPhone automatically.

## Expected first-run failure points

- Unity licence activation / Unity version availability in `game-ci/unity-builder`.
- Unity package resolution or project compilation.
- Xcode project export/build errors.
- Signing is intentionally not attempted in the cloud workflow.

The workflow has been statically reviewed against this source tree, but it has **not** been executed in the user's GitHub account. Do not treat the IPA as built until the Actions run succeeds.
