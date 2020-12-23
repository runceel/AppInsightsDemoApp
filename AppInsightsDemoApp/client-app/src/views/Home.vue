<template>
  <div class="home">
      <button class="uk-button uk-button-default" @click="updateLabel">ok</button>
      <input type="text" class="uk-input" v-model="state.label" />
    <span>{{ state.label }}</span>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue"

type State = {
  label: string;
};

export default defineComponent({
  name: "Home",
  components: {
  },
  setup() {
    const state = reactive<State>({
      label: "",
    })

    const updateLabel = () => state.label = new Date().toString()

    return {
      state,
      updateLabel,
    }
  },
});
</script>
