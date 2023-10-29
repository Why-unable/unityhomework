environment:

```
mac m1 -->parallels desktop -->win11 -->visual studio 17 2022 && cmake
```

build

```
cd DrawSVG 
cmake -B build -G "Visual Studio 17 2022" -A x64
cd build
## open drawsvg.sln in vs2022
in vs 2022,make sure that "x64" is selected near "debug" button.
then, run or debug,after it is finished, an exe file "drawsvg.exe" 
    could be found in folder build/Debug
```

 