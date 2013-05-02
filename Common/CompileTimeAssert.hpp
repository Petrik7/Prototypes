#pragma once

template<bool>
class CompileTimeAssert;
 
template<>
struct CompileTimeAssert<true> {};

