import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'
import Toast from 'primevue/toast'
import InputText from 'primevue/inputtext'
import Button from "primevue/button";
import 'primeflex/primeflex.css'
import 'primevue/resources/themes/saga-blue/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'

createApp(App)
    .use(router)
    .use(PrimeVue)
    .use(ToastService)
    .component('Toast', Toast)
    .component('InputText', InputText)
    .component('Button', Button)
    .mount('#app')
