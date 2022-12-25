# Bloons TD Save decrypt/encrypt tool

This program allows will encrypt/encrypt and calculate checksums for Bloons TD save files (Profile.save, OldProfile.save, CloudSave, events.data)

Usage:

```
BloonsCrypt [-d --decrypt | -e --encrypt] <input file> <output file>
```


To decrypt a save:


```
BloonsCrypt -d Profile.save Profile_decrypted.save 
```

To encrypt + calculate checksum:

```
BloonsCrypt -e Profile_edited.save Profile.save 
```

All of files named above are JSON formatted when decrypted.

