# FileDuplicatedFinder
Find duplicate files under the folder specified using MD5 hashing (Windows .NET 4.5)

The purpose of this program is to elminate duplicate files in a user folder such as Downloads or Pictures.
This program does this by keeping track of all the MD5 hashes of the files located under the foler specified.
When it detects a identical MD5 hash, it tracks this and attempts to determine if the file is a automaticly named copy.
If it is a automatically named copy, it is cosidered a copy and this is tracked. If the program can not determine
if it is an automatically generated copy it is cosidered a duplicate.

The delete button will delete (Send to recycle bin) all copies and duplicates encounted after the first hash.

Future work:
Allow option to delete copies vs duplicates or both.
Log deletions done
Undo option using log
