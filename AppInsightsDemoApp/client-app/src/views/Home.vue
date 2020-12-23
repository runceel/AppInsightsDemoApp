<template>
    <div class="home">
        <Button type="submit" label="Submit" />
        <InputText type="text" v-model="state.label" />
        <span>{{ state.label }}</span>
    </div>
</template>

<script lang="ts">
    import { defineComponent, reactive } from 'vue';
    import Button from 'primevue/button'
    import InputText from 'primevue/inputtext'

    type State = {
        label: string
    }

    export default defineComponent({
        name: 'Home',
        components: {
            Button,
            InputText,
        },
        setup() {
            const state = reactive<State>({
                label: ""
            })

            return {
                state,
            }
        },
    });
</script>
