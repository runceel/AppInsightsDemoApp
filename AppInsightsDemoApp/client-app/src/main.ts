import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import 'uikit/dist/css/uikit.css'

createApp(App)
    .use(router)
    .mount('#app')
