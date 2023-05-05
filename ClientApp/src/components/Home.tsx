import * as React from "react";
import { connect } from "react-redux";

const Home = () => (
  <div className="divs">
    <div>Div1</div>
    <div>Div2</div>
    <div>Div3</div>
  </div>
);

export default connect()(Home);
