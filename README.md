# FileDuplicatedFinder
Find duplicate files under the folder specified using MD5 hashing (C# Windows .NET 4.5)

The purpose of this program is to elminate duplicate files in a user folder such as Downloads or Pictures.
This program does this by keeping track of all the MD5 hashes of the files located under the foler specified.
When it detects a identical MD5 hash, it tracks this and attempts to determine if the file is a automaticly named copy.
If it is a automatically named copy, it is cosidered a copy and this is tracked. If the program can not determine
if it is an automatically generated copy it is cosidered a duplicate.

The delete button will delete (Send to recycle bin) all copies and duplicates encounted after the first hash.
The undo option will read undo information from a log file that is generated in the base directory upon deletion.
It will then replace every copy that was deleted by copying and renaming files as necessary.

Future work:

Error handling - file access issues not handled right now

Deletion optoins - delete vs send to recycle bin and duplicates/copies

Restore option - duplicates/copies or both.
