import ReactDOM from 'react-dom/client';
import App from './App';
import store from './redux/store'
import { Provider } from 'react-redux'
import {
  QueryClient,
  QueryClientProvider,
} from '@tanstack/react-query'

// Components
import MainInitializer from './initializers/MainInitializer';

// Css
import "./normalize.css"
import "./index.css"

const queryClient = new QueryClient()

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <QueryClientProvider client={queryClient}>
      <MainInitializer>
        <App />
      </MainInitializer>
    </QueryClientProvider>
  </Provider>
);