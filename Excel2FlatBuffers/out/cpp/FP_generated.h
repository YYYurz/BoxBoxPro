// automatically generated by the FlatBuffers compiler, do not modify


#ifndef FLATBUFFERS_GENERATED_FP_GAMECONFIG_H_
#define FLATBUFFERS_GENERATED_FP_GAMECONFIG_H_

#include "flatbuffers/flatbuffers.h"

namespace GameConfig {

struct FP;

FLATBUFFERS_MANUALLY_ALIGNED_STRUCT(8) FP FLATBUFFERS_FINAL_CLASS {
 private:
  int64_t raw_;

 public:
  FP() {
    memset(static_cast<void *>(this), 0, sizeof(FP));
  }
  FP(int64_t _raw)
      : raw_(flatbuffers::EndianScalar(_raw)) {
  }
  int64_t raw() const {
    return flatbuffers::EndianScalar(raw_);
  }
};
FLATBUFFERS_STRUCT_END(FP, 8);

}  // namespace GameConfig

#endif  // FLATBUFFERS_GENERATED_FP_GAMECONFIG_H_
