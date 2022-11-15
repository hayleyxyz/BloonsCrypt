### Bloons TD Save decrypt/encrypt tool

This program allows will encrypt/encrypt and calculate checksums for Bloons TD save files (Profile.save, OldProfile.save, CloudSave, events.data)


To decrypt a save:


```
btd5crypt.exe -d Profile.save Profile_decrypted.save 
```

To encrypt + calculate checksum:

```
btd5crypt.exe -e Profile_edited.save Profile.save 
```

