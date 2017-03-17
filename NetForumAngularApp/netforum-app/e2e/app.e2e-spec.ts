import { NetforumAppPage } from './app.po';

describe('netforum-app App', () => {
  let page: NetforumAppPage;

  beforeEach(() => {
    page = new NetforumAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
