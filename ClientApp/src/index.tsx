import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createBrowserHistory } from "history";
import configureStore from "./store/configureStore";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";
import "./i18n";
// Create browser history to use in the Redux store
const baseUrl = document
  .getElementsByTagName("base")[0]
  .getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = configureStore(history);

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);

registerServiceWorker();
