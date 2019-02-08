# PatternScanner

This is a utility that allows for rapid development and testing of patterns.
PatternScanner is able to import patterns from:
* IDA (`.<section>:<address> <opcodes> <disassembly>`)
* Cheat-Engine (`<address> - <opcodes> - <disassembly>`)
* Hex (hexadecimal numbers and '?' or '??' as wildcards)

By using [PeNet](https://github.com/secana/PeNet), PatternScanner is able to scan single sections of files only.
When unable to parse sections or when the input file could not be parsed as a PE file, it will scan the entire file.
